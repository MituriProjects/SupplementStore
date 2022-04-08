using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class ProductExtensions {

        public static ProductDetails ToDetails(this Product product, ProductImage productImage = null) {

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price,
                MainImage = productImage?.Name
            };
        }
    }
}
