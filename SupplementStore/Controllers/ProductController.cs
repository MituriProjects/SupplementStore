﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Controllers.Services;
using SupplementStore.ViewModels.Product;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    public class ProductController : Controller {

        IProductService ProductService { get; }

        IProductImagesProvider ProductImagesProvider { get; }

        IProductImageCreator ProductImageCreator { get; }

        IProductImageRemover ProductImageRemover { get; }

        IMainProductImageAppointer MainProductImageAppointer { get; }

        IFileWriter FileWriter { get; }

        public ProductController(
            IProductService productService,
            IProductImagesProvider productImagesProvider,
            IProductImageCreator productImageCreator,
            IProductImageRemover productImageRemover,
            IMainProductImageAppointer mainProductImageAppointer,
            IFileWriter fileWriter) {

            ProductService = productService;
            ProductImagesProvider = productImagesProvider;
            ProductImageCreator = productImageCreator;
            ProductImageRemover = productImageRemover;
            MainProductImageAppointer = mainProductImageAppointer;
            FileWriter = fileWriter;
        }

        public IActionResult Index(ProductIndexViewModel model) {

            model = model ?? new ProductIndexViewModel();

            var loadedProducts = ProductService.LoadMany(new ProductsProviderArgs {
                Skip = model.Skip,
                Take = model.Take
            });

            model.AllProductsCount = loadedProducts.AllProductsCount;
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

            return View(new ProductDetailsViewModel {
                Product = product,
                Opinions = ProductService.LoadOpinions(product.Id),
                Images = ProductImagesProvider.Load(product.Id).OrderByDescending(e => e.IsMain).Select(e => e.Name)
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() {

            return View("Edit", new ProductEditViewModel());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id) {

            var product = ProductService.Load(id);

            return View(new ProductEditViewModel {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductEditViewModel model) {

            if (ModelState.IsValid == false)
                return View(model);

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

            var productImageCreatorResult = ProductImageCreator.Create(productId, file.FileName);

            if (productImageCreatorResult.Success) {

                await FileWriter.ProcessAsync(file, "productImages", productId);
            }

            return RedirectToAction(nameof(Details), new { Id = productId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SetImageAsMain(string productId, string imageName) {

            MainProductImageAppointer.Perform(productId, imageName);

            return RedirectToAction(nameof(Details), new { Id = productId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveImage(string productId, string imageName) {

            var productImageRemoverResult = ProductImageRemover.Remove(productId, imageName);

            if (productImageRemoverResult.Success) {

                FileWriter.Delete(imageName, "productImages", productId);
            }

            return RedirectToAction(nameof(Details), new { Id = productId });
        }
    }
}
