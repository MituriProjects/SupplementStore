using SupplementStore.Application.Services;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService : IOrderService {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        OrderFactory OrderFactory { get; }

        IDomainApprover DomainApprover { get; }

        public OrderService(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            OrderFactory orderFactory,
            IDomainApprover domainApprover) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OrderFactory = orderFactory;
            DomainApprover = domainApprover;
        }
    }
}
