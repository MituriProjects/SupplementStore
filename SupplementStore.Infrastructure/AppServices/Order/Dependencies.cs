using SupplementStore.Application.Services;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService : IOrderService {

        IAddressRepository AddressRepository { get; }

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        OrderFactory OrderFactory { get; }

        IDomainApprover DomainApprover { get; }

        public OrderService(
            IAddressRepository addressRepository,
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            OrderFactory orderFactory,
            IDomainApprover domainApprover) {

            AddressRepository = addressRepository;
            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OrderFactory = orderFactory;
            DomainApprover = domainApprover;
        }
    }
}
