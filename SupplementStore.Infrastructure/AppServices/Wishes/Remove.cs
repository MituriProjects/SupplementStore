using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Wishes {

    public partial class WishService {

        public void Remove(string userId, string productId) {

            WishRepository.Delete(userId, new ProductId(productId));

            DomainApprover.SaveChanges();
        }
    }
}
