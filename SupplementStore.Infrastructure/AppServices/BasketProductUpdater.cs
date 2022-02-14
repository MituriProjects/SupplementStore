using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using System;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductUpdater : IBasketProductUpdater {

        IRepository<BasketProduct> BasketProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductUpdater(
            IRepository<BasketProduct> basketProductRepository,
            IDocumentApprover documentApprover) {

            BasketProductRepository = basketProductRepository;
            DocumentApprover = documentApprover;
        }

        public void Update(BasketProductDetails basketProductDetails) {

            if (Guid.TryParse(basketProductDetails.Id, out var guidBasketProductId) == false)
                return;

            var basketProduct = BasketProductRepository.FindBy(guidBasketProductId);

            if (basketProduct == null)
                return;

            basketProduct.Quantity = basketProductDetails.Quantity;

            DocumentApprover.SaveChanges();
        }
    }
}
