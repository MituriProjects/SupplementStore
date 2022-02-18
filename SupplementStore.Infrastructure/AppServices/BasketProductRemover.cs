﻿using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductRemover : IBasketProductRemover {

        IBasketProductRepository BasketProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public BasketProductRemover(
            IBasketProductRepository basketProductRepository,
            IDocumentApprover documentApprover) {

            BasketProductRepository = basketProductRepository;
            DocumentApprover = documentApprover;
        }

        public void Remove(string userId, string productId) {

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, new ProductId(productId)));

            if (basketProduct == null)
                return;

            BasketProductRepository.Delete(basketProduct.BasketProductId);

            DocumentApprover.SaveChanges();
        }
    }
}