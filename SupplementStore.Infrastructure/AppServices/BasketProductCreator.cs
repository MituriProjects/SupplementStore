using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductCreator : IBasketProductCreator {

        IRepository<Product> ProductRepository { get; }

        IRepository<BasketProduct> BasketProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductCreator(
            IRepository<Product> productRepository,
            IRepository<BasketProduct> basketProductRepository,
            IDocumentApprover documentApprover) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
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

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, guidProductId));

            if (basketProduct == null) {

                try {

                    basketProduct = new BasketProduct {
                        UserId = userId,
                        ProductId = guidProductId,
                        Quantity = quantity
                    };
                }
                catch {

                    return;
                }

                BasketProductRepository.Add(basketProduct);
            }
            else {

                basketProduct.Quantity += quantity;
            }

            DocumentApprover.SaveChanges();
        }
    }
}
