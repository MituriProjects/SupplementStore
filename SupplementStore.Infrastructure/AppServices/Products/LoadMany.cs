using SupplementStore.Application.Args;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Shared;
using SupplementStore.Infrastructure.ModelMapping;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService {

        public ProductsProvideResult LoadMany(ProductsProvideArgs args) {

            var products = ProductRepository
                .FindBy(new PagingFilter<Product>(args.Skip, args.Take))
                .Select(e => e.ToDetails(ProductImageRepository.FindBy(new MainProductImageFilter(e.ProductId))));

            return new ProductsProvideResult {
                AllProductsCount = ProductRepository.Count(),
                Products = products
            };
        }
    }
}
