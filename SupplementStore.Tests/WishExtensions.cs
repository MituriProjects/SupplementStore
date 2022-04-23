using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Tests {

    static class WishExtensions {

        public static Wish WithUserId(this Wish wish, IdentityUser user) {

            wish.UserId = user.Id;

            return wish;
        }

        public static Wish WithProductId(this Wish wish, Product product) {

            wish.ProductId = product.ProductId;

            return wish;
        }
    }
}
