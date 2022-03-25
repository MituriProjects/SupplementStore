using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductCreator : IProductCreator {

        IProductRepository ProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public ProductCreator(
            IProductRepository productRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            DomainApprover = domainApprover;
        }

        public ProductDetails Create(string name, decimal price) {

            var product = new Product {
                Name = name,
                Price = price
            };

            ProductRepository.Add(product);

            DomainApprover.SaveChanges();

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
