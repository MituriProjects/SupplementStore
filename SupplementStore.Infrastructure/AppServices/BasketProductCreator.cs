using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductCreator : IBasketProductCreator {

        BasketProductManager BasketProductManager { get; }

        IDomainApprover DomainApprover { get; }

        public BasketProductCreator(
            BasketProductManager basketProductManager,
            IDomainApprover domainApprover) {

            BasketProductManager = basketProductManager;
            DomainApprover = domainApprover;
        }

        public void Create(string userId, string productId, int quantity) {

            BasketProductManager.Adjust(userId, productId, quantity);

            DomainApprover.SaveChanges();
        }
    }
}
