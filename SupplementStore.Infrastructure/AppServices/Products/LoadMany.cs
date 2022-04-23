using SupplementStore.Application.Args;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.ModelMapping;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService {

        public ProductsProviderResult LoadMany(ProductsProvideArgs args) {

            var products = ProductRepository.Entities
                .Skip(args.Skip)
                .Take(args.Take)
                .Select(e => e.ToDetails(ProductImageRepository.FindBy(new MainProductImageFilter(e.ProductId))));

            return new ProductsProviderResult {
                AllProductsCount = ProductRepository.Count(),
                Products = products
            };
        }
    }
}
