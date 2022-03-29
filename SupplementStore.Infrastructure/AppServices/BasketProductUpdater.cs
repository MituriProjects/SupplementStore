using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductUpdater : IBasketProductUpdater {

        IBasketProductRepository BasketProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public BasketProductUpdater(
            IBasketProductRepository basketProductRepository,
            IDomainApprover domainApprover) {

            BasketProductRepository = basketProductRepository;
            DomainApprover = domainApprover;
        }

        public void Update(BasketProductDetails basketProductDetails) {

            var basketProduct = BasketProductRepository.FindBy(new BasketProductId(basketProductDetails.Id));

            if (basketProduct == null)
                return;

            basketProduct.Quantity = basketProductDetails.Quantity;

            DomainApprover.SaveChanges();
        }
    }
}
