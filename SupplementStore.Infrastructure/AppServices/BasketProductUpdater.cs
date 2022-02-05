using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductUpdater : IBasketProductUpdater {

        IDocument<BasketProduct> BasketProductDocument { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductUpdater(
            IDocument<BasketProduct> basketProductDocument,
            IDocumentApprover documentApprover) {

            BasketProductDocument = basketProductDocument;
            DocumentApprover = documentApprover;
        }

        public void Update(BasketProductDetails basketProductDetails) {

            var basketProduct = BasketProductDocument.All
                .FirstOrDefault(e => e.Id == Guid.Parse(basketProductDetails.Id));

            if (basketProduct == null)
                return;

            basketProduct.Quantity = basketProductDetails.Quantity;

            DocumentApprover.SaveChanges();
        }
    }
}
