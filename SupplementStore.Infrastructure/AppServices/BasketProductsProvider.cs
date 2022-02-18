using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductsProvider : IBasketProductsProvider {

        IProductRepository ProductRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        public BasketProductsProvider(
            IProductRepository productRepository,
            IBasketProductRepository basketProductRepository) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
        }

        public IEnumerable<BasketProductDetails> Load(string userId) {

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(userId))) {

                var product = ProductRepository.FindBy(basketProduct.ProductId);

                yield return new BasketProductDetails {
                    Id = basketProduct.BasketProductId.ToString(),
                    ProductId = basketProduct.ProductId.ToString(),
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    Quantity = basketProduct.Quantity
                };
            }
        }
    }
}
