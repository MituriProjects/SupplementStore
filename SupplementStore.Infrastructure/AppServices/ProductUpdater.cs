using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductUpdater : IProductUpdater {

        IProductRepository ProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public ProductUpdater(
            IProductRepository productRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            DomainApprover = domainApprover;
        }

        public void Update(ProductDetails productDetails) {

            var product = ProductRepository.FindBy(new ProductId(productDetails.Id));

            if (product == null)
                return;

            product.Name = productDetails.Name;
            product.Price = productDetails.Price;

            DomainApprover.SaveChanges();
        }
    }
}
