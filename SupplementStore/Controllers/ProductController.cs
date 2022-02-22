using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Product;

namespace SupplementStore.Controllers {

    public class ProductController : Controller {

        IProductProvider ProductProvider { get; }

        IProductsProvider ProductsProvider { get; }

        IProductCreator ProductCreator { get; }

        IProductUpdater ProductUpdater { get; }

        public ProductController(
            IProductProvider productProvider,
            IProductsProvider productsProvider,
            IProductCreator productCreator,
            IProductUpdater productUpdater) {

            ProductProvider = productProvider;
            ProductsProvider = productsProvider;
            ProductCreator = productCreator;
            ProductUpdater = productUpdater;
        }

        public IActionResult Index(ProductIndexViewModel model) {

            var loadedProducts = ProductsProvider.Load(new ProductsProviderArgs {
                Skip = model.Skip,
                Take = model.Take
            });

            model.AllProductsCount = loadedProducts.AllProductsCount;
            model.Products = loadedProducts.Products;

            return View(model);
        }

        public IActionResult Details(string id) {

            var product = ProductProvider.Load(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        [Authorize(Roles = "Owner, Admin")]
        public IActionResult Create() {

            return View("Edit", new ProductEditViewModel());
        }
    }
}
