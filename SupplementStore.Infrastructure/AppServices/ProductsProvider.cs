using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities;
using SupplementStore.Domain.Entities.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductsProvider : IProductsProvider {

        IRepository<Product> ProductRepository { get; }

        public ProductsProvider(IRepository<Product> productRepository) {

            ProductRepository = productRepository;
        }

        public ProductsProviderResult Load(ProductsProviderArgs args) {

            var products = ProductRepository.Entities
                .Skip(args.Skip)
                .Take(args.Take)
                .Select(e => new ProductDetails {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    Price = e.Price
                });

            return new ProductsProviderResult {
                AllProductsCount = ProductRepository.Count(),
                Products = products
            };
        }
    }
}
