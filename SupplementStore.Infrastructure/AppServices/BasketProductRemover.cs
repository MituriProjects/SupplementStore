using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductRemover : IBasketProductRemover {

        IDocument<BasketProduct> BasketProductDocument { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductRemover(
            IDocument<BasketProduct> basketProductDocument,
            IDocumentApprover documentApprover) {

            BasketProductDocument = basketProductDocument;
            DocumentApprover = documentApprover;
        }

        public void Remove(string userId, string productId) {

            var basketProduct = BasketProductDocument.All.FirstOrDefault(e => e.UserId == userId && e.ProductId == Guid.Parse(productId));

            if (basketProduct == null)
                return;

            BasketProductDocument.Delete(basketProduct.Id);

            DocumentApprover.SaveChanges();
        }
    }
}
