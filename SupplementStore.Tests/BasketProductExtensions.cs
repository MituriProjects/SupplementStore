using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class BasketProductExtensions {

        public static BasketProduct WithUserId(this BasketProduct basketProduct, string userId) {

            basketProduct.UserId = userId;

            return basketProduct;
        }

        public static BasketProduct WithProductId(this BasketProduct basketProduct, ProductId productId) {

            basketProduct.ProductId = productId;

            return basketProduct;
        }

        public static BasketProduct WithQuantity(this BasketProduct basketProduct, int quantity) {

            basketProduct.Quantity = quantity;

            return basketProduct;
        }
    }
}
