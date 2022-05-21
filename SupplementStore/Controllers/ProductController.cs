using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Controllers.Filters;
using SupplementStore.Controllers.Services;
using SupplementStore.ViewModels.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    public class ProductController : AppControllerBase {

        IProductService ProductService { get; }

        IProductImageService ProductImageService { get; }

        IFileManager FileWriter { get; }

        public ProductController(
            IProductService productService,
            IProductImageService productImageService,
            IFileManager fileWriter) {

            ProductService = productService;
            ProductImageService = productImageService;
            FileWriter = fileWriter;
        }

        public IActionResult Index(IndexVM model) {

            model = model ?? new IndexVM();

            var loadedProducts = ProductService.LoadMany(new ProductsProvideArgs {
                Skip = model.Page.Skip,
                Take = model.Page.Take
            });

            model.Page.Count = loadedProducts.AllProductsCount;
            model.Products = loadedProducts.Products;

            foreach (var product in model.Products) {

                var opinions = ProductService.LoadOpinions(product.Id);

                model.ProductRatings[product.Id] = new ProductRating {
                    Average = opinions.Count() == 0 ? 0 : Math.Round(opinions.Average(e => e.Stars), 2),
                    Count = opinions.Count()
                };
            }

            return View(model);
        }

        public IActionResult Details(string id) {

            var product = ProductService.Load(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(new DetailsVM {
                Product = product,
                Opinions = ProductService.LoadOpinions(product.Id),
                Images = ProductImageService.LoadMany(product.Id).OrderByDescending(e => e.IsMain).Select(e => e.Name)
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() {

            return View("Edit", new EditVM());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id) {

            var product = ProductService.Load(id);

            return View(new EditVM {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ReturnToViewOnModelInvalid]
        public IActionResult Edit(EditVM model) {

            if (string.IsNullOrEmpty(model.Id)) {

                var productDetails = ProductService.Create(model.Name, model.Price.Value);

                return RedirectToAction(nameof(Details), new { productDetails.Id });
            }

            ProductService.Update(new ProductDetails {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price.Value
            });

            return RedirectToAction(nameof(Details), new { model.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddImage(string productId, IFormFile file) {

            if (file == null)
                return RedirectToAction(nameof(Details), new { Id = productId });

            var productImageCreateResult = ProductImageService.Create(productId, file.FileName);

            if (productImageCreateResult.Success) {

                await FileWriter.SaveAsync(file, "productImages", productId);
            }

            return RedirectToAction(nameof(Details), new { Id = productId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SetImageAsMain(string productId, string imageName) {

            ProductImageService.AppointMain(productId, imageName);

            return RedirectToAction(nameof(Details), new { Id = productId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveImage(string productId, string imageName) {

            var productImageRemoveResult = ProductImageService.Remove(productId, imageName);

            if (productImageRemoveResult.Success) {

                FileWriter.Delete(imageName, "productImages", productId);
            }

            return RedirectToAction(nameof(Details), new { Id = productId });
        }
    }
}
