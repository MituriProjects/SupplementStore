using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices {

    public class WishProvider : IWishProvider {

        IWishRepository WishRepository { get; }

        public WishProvider(IWishRepository wishRepository) {

            WishRepository = wishRepository;
        }

        public bool Load(string userId, string productId) {

            var wish = WishRepository.FindBy(new UserWishFilter(userId, new ProductId(productId)));

            return wish != null;
        }
    }
}
