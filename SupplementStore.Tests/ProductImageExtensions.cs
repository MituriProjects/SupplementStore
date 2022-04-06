using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class ProductImageExtensions {

        public static ProductImage WithProductId(this ProductImage productImage, Product product) {

            productImage.ProductId = product.ProductId;

            return productImage;
        }

        public static ProductImage WithName(this ProductImage productImage, string name) {

            productImage.Name = name;

            return productImage;
        }
    }
}
