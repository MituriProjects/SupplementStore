using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductUpdater : IBasketProductUpdater {

        IBasketProductRepository BasketProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductUpdater(
            IBasketProductRepository basketProductRepository,
            IDocumentApprover documentApprover) {

            BasketProductRepository = basketProductRepository;
            DocumentApprover = documentApprover;
        }

        public void Update(BasketProductDetails basketProductDetails) {

            var basketProduct = BasketProductRepository.FindBy(new BasketProductId(basketProductDetails.Id));

            if (basketProduct == null)
                return;

            basketProduct.Quantity = basketProductDetails.Quantity;

            DocumentApprover.SaveChanges();
        }
    }
}
