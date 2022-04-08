using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductsProvider : IProductsProvider {

        IProductRepository ProductRepository { get; }

        IProductImageRepository ProductImageRepository { get; }

        public ProductsProvider(
            IProductRepository productRepository,
            IProductImageRepository productImageRepository) {

            ProductRepository = productRepository;
            ProductImageRepository = productImageRepository;
        }

        public ProductsProviderResult Load(ProductsProviderArgs args) {

            var products = ProductRepository.Entities
                .Skip(args.Skip)
                .Take(args.Take)
                .Select(e => new ProductDetails {
                    Id = e.ProductId.ToString(),
                    Name = e.Name,
                    Price = e.Price,
                    MainImage = ProductImageRepository.FindBy(new MainProductImageFilter(e.ProductId))?.Name
                });

            return new ProductsProviderResult {
                AllProductsCount = ProductRepository.Count(),
                Products = products
            };
        }
    }
}
