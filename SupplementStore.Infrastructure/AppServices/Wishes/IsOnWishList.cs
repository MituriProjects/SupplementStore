using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices.Wishes {

    public partial class WishService {

        public bool IsOnWishList(string userId, string productId) {

            var wish = WishRepository.FindBy(new UserWishFilter(userId, new ProductId(productId)));

            return wish != null;
        }
    }
}
