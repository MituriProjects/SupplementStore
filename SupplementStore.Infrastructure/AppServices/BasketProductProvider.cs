using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductProvider : IBasketProductProvider {

        IRepository<Product> ProductRepository { get; }

        IRepository<BasketProduct> BasketProductRepository { get; }

        public BasketProductProvider(
            IRepository<Product> productRepository,
            IRepository<BasketProduct> basketProductRepository) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
        }

        public BasketProductDetails Load(string id) {

            var basketProduct = BasketProductRepository.FindBy(Guid.Parse(id));

            if (basketProduct == null)
                return null;

            var product = ProductRepository.FindBy(basketProduct.ProductId);

            if (product == null)
                return null;

            return new BasketProductDetails {
                Id = basketProduct.Id.ToString(),
                ProductId = basketProduct.ProductId.ToString(),
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = basketProduct.Quantity
            };
        }
    }
}
