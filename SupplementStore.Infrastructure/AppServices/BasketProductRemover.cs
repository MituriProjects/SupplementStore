using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using System;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductRemover : IBasketProductRemover {

        IRepository<BasketProduct> BasketProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductRemover(
            IRepository<BasketProduct> basketProductRepository,
            IDocumentApprover documentApprover) {

            BasketProductRepository = basketProductRepository;
            DocumentApprover = documentApprover;
        }

        public void Remove(string userId, string productId) {

            if (Guid.TryParse(productId, out Guid guidProductId) == false)
                return;

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, guidProductId));

            if (basketProduct == null)
                return;

            BasketProductRepository.Delete(basketProduct.Id);

            DocumentApprover.SaveChanges();
        }
    }
}
