using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductCreator : IBasketProductCreator {

        IRepository<Product> ProductRepository { get; }

        IDocument<BasketProduct> BasketProductDocument { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductCreator(
            IRepository<Product> productRepository,
            IDocument<BasketProduct> basketProductDocument,
            IDocumentApprover documentApprover) {

            ProductRepository = productRepository;
            BasketProductDocument = basketProductDocument;
            DocumentApprover = documentApprover;
        }

        public void Create(string userId, string productId, int quantity) {

            if (string.IsNullOrEmpty(userId))
                return;

            if (Guid.TryParse(productId, out var guidProductId) == false)
                return;

            var product = ProductRepository.FindBy(guidProductId);

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
