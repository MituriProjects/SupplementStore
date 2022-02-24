using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductCreator : IProductCreator {

        IProductRepository ProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public ProductCreator(
            IProductRepository productRepository,
            IDocumentApprover documentApprover) {

            ProductRepository = productRepository;
            DocumentApprover = documentApprover;
        }

        public ProductDetails Create(string name, decimal price) {

            var product = new Product {
                Name = name,
                Price = price
            };

            ProductRepository.Add(product);

            DocumentApprover.SaveChanges();

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
