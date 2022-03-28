using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppModels {

    static class ProductDetailsFactory {

        public static ProductDetails Create(Product product) {

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
