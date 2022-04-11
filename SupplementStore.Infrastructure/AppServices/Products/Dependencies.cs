using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService : IProductService {

        IProductRepository ProductRepository { get; }

        IProductImageRepository ProductImageRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        IDomainApprover DomainApprover { get; }

        public ProductService(
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            IPurchaseRepository purchaseRepository,
            IOpinionRepository opinionRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            ProductImageRepository = productImageRepository;
            PurchaseRepository = purchaseRepository;
            OpinionRepository = opinionRepository;
            DomainApprover = domainApprover;
        }
    }
}
