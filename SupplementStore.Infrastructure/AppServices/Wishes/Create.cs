using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices.Wishes {

    public partial class WishService {

        public void Create(string userId, string productId) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return;

            var wish = WishRepository.FindBy(new UserWishFilter(userId, new ProductId(productId)));

            if (wish != null)
                return;

            WishRepository.Add(new Wish {
                UserId = userId,
                ProductId = new ProductId(productId)
            });

            DomainApprover.SaveChanges();
        }
    }
}
