using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductCreator : IBasketProductCreator {

        IDocument<Product> ProductDocument { get; }

        IDocument<BasketProduct> BasketProductDocument { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductCreator(
            IDocument<Product> productDocument,
            IDocument<BasketProduct> basketProductDocument,
            IDocumentApprover documentApprover) {

            ProductDocument = productDocument;
            BasketProductDocument = basketProductDocument;
            DocumentApprover = documentApprover;
        }

        public void Create(string userId, string productId, int quantity) {

            if (string.IsNullOrEmpty(userId))
                return;

            if (Guid.TryParse(productId, out var guidProductId) == false)
                return;

            var product = ProductDocument.All
                .FirstOrDefault(e => e.Id == guidProductId);

            if (product == null)
                return;

            var basketProduct = BasketProductDocument.All
                .FirstOrDefault(e => e.UserId == userId && e.ProductId == guidProductId);

            if (basketProduct == null) {

                basketProduct = new BasketProduct {
                    UserId = userId,
                    ProductId = guidProductId,
                    Quantity = quantity
                };

                if (IsValid(basketProduct))
                    BasketProductDocument.Add(basketProduct);
            }
            else {

                basketProduct.Quantity += quantity;
            }

            if (IsValid(basketProduct))
                DocumentApprover.SaveChanges();
        }

        private bool IsValid(BasketProduct basketProduct) {

            return basketProduct.GetBrokenRules().Count() == 0;
        }
    }
}
