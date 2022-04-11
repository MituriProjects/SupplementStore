using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService : IProductImageService {

        IProductRepository ProductRepository { get; }

        IProductImageRepository ProductImageRepository { get; }

        IDomainApprover DomainApprover { get; }

        public ProductImageService(
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            ProductImageRepository = productImageRepository;
            DomainApprover = domainApprover;
        }
    }
}
