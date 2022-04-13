using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService : IOpinionService {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OpinionService(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository,
            IDomainApprover domainApprover) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
            DomainApprover = domainApprover;
        }
    }
}
