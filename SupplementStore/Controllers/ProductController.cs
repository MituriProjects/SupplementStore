using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Product;

namespace SupplementStore.Controllers {

    public class ProductController : Controller {

        IProductProvider ProductProvider { get; }

        public ProductController(IProductProvider productProvider) {

            ProductProvider = productProvider;
        }

        public IActionResult Index(ProductIndexViewModel model) {

            var loadedProducts = ProductProvider.Load(new ProductProviderArgs {
                Skip = model.Skip,
                Take = model.Take
            });

            model.AllProductsCount = loadedProducts.AllProductsCount;
            model.Products = loadedProducts.Products;

            return View(model);
        }
    }
}
