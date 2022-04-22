using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class BasketProductExtensions {

        public static BasketProduct WithUserId(this BasketProduct basketProduct, IdentityUser user) {

            basketProduct.UserId = user.Id;

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
