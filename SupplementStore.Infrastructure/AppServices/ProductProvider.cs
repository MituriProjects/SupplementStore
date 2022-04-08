using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductProvider : IProductProvider {

        IProductRepository ProductRepository { get; }

        public ProductProvider(IProductRepository productRepository) {

            ProductRepository = productRepository;
        }

        public ProductDetails Load(string productId) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return null;

            return product.ToDetails();
        }
    }
}
