using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.AppModels;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService {

        public ProductDetails Create(string name, decimal price) {

            var product = new Product {
                Name = name,
                Price = price
            };

            ProductRepository.Add(product);

            DomainApprover.SaveChanges();

            return ProductDetailsFactory.Create(product);
        }
    }
}
