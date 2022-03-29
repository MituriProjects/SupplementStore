using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices {

    public class WishRemover : IWishRemover {

        IWishRepository WishRepository { get; }

        IDomainApprover DomainApprover { get; }

        public WishRemover(
            IWishRepository wishRepository,
            IDomainApprover domainApprover) {

            WishRepository = wishRepository;
            DomainApprover = domainApprover;
        }

        public void Remove(string userId, string productId) {

            WishRepository.Delete(userId, new ProductId(productId));

            DomainApprover.SaveChanges();
        }
    }
}
