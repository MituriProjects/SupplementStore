using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices {

    public class WishRemover : IWishRemover {

        IWishRepository WishRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public WishRemover(
            IWishRepository wishRepository,
            IDocumentApprover documentApprover) {

            WishRepository = wishRepository;
            DocumentApprover = documentApprover;
        }

        public void Remove(string userId, string productId) {

            WishRepository.Delete(userId, new ProductId(productId));

            DocumentApprover.SaveChanges();
        }
    }
}
