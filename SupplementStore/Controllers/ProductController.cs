using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Product;

namespace SupplementStore.Controllers {

    public class ProductController : Controller {

        IProductProvider ProductProvider { get; }

        IProductsProvider ProductsProvider { get; }

        public ProductController(
            IProductProvider productProvider,
            IProductsProvider productsProvider) {

            ProductProvider = productProvider;
            ProductsProvider = productsProvider;
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
    }
}
