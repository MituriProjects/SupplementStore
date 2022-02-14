using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Products;
using System;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductProvider : IProductProvider {

        IRepository<Product> ProductRepository { get; }

        public ProductProvider(IRepository<Product> productRepository) {

            ProductRepository = productRepository;
        }

        public ProductDetails Load(string productId) {

            if (Guid.TryParse(productId, out var guidProductId) == false)
                return null;

            var product = ProductRepository.FindBy(guidProductId);

            if (product == null)
                return null;

            return new ProductDetails {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
