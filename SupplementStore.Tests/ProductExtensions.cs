using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class ProductExtensions {

        public static Product WithName(this Product product, string name) {

            product.Name = name;

            return product;
        }

        public static Product WithPrice(this Product product, decimal price) {

            product.Price = price;

            return product;
        }
    }
}
