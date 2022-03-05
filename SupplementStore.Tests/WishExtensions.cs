using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Tests {

    static class WishExtensions {

        public static Wish WithUserId(this Wish wish, string userId) {

            wish.UserId = userId;

            return wish;
        }

        public static Wish WithProductId(this Wish wish, ProductId productId) {

            wish.ProductId = productId;

            return wish;
        }
    }
}
