using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Product;
using System;
using System.Linq;

namespace SupplementStore.Controllers {

    public class ProductController : Controller {

        IProductProvider ProductProvider { get; }

        IProductsProvider ProductsProvider { get; }

        IProductOpinionsProvider ProductOpinionsProvider { get; }

        IProductCreator ProductCreator { get; }

        IProductUpdater ProductUpdater { get; }

        public ProductController(
            IProductProvider productProvider,
            IProductsProvider productsProvider,
            IProductOpinionsProvider productOpinionsProvider,
            IProductCreator productCreator,
            IProductUpdater productUpdater) {

            ProductProvider = productProvider;
            ProductsProvider = productsProvider;
            ProductOpinionsProvider = productOpinionsProvider;
            ProductCreator = productCreator;
            ProductUpdater = productUpdater;
        }

        public IActionResult Index(ProductIndexViewModel model) {

            model = model ?? new ProductIndexViewModel();

            var loadedProducts = ProductsProvider.Load(new ProductsProviderArgs {
                Skip = model.Skip,
                Take = model.Take
            });

            model.AllProductsCount = loadedProducts.AllProductsCount;
            model.Products = loadedProducts.Products;

            foreach (var product in model.Products) {

                var opinions = ProductOpinionsProvider.Load(product.Id);

                model.ProductGrades[product.Id] = new ProductGrade {
                    Average = opinions.Count() == 0 ? 0 : Math.Round(opinions.Average(e => e.Stars), 2),
                    Count = opinions.Count()
                };
            }

            return View(model);
        }

        public IActionResult Details(string id) {

            var product = ProductProvider.Load(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(new ProductDetailsViewModel {
                Product = product,
                Opinions = ProductOpinionsProvider.Load(product.Id)
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() {

            return View("Edit", new ProductEditViewModel());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id) {

            var product = ProductProvider.Load(id);

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

                var productDetails = ProductCreator.Create(model.Name, model.Price.Value);

                return RedirectToAction(nameof(Details), new { productDetails.Id });
            }

            ProductUpdater.Update(new ProductDetails {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price.Value
            });

            return RedirectToAction(nameof(Details), new { model.Id });
        }
    }
}
