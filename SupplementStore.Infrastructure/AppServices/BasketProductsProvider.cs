using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductsProvider : IBasketProductsProvider {

        IRepository<Product> ProductRepository { get; }

        IRepository<BasketProduct> BasketProductRepository { get; }

        public BasketProductsProvider(
            IRepository<Product> productRepository,
            IRepository<BasketProduct> basketProductRepository) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
        }

        public IEnumerable<BasketProductDetails> Load(string userId) {

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(userId))) {

                var product = ProductRepository.FindBy(basketProduct.ProductId);

                yield return new BasketProductDetails {
                    Id = basketProduct.Id.ToString(),
                    ProductId = basketProduct.ProductId.ToString(),
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    Quantity = basketProduct.Quantity
                };
            }
        }
    }
}
