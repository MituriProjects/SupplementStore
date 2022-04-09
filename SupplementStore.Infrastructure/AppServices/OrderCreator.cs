using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Orders;
using SupplementStore.Infrastructure.ArgsMapping;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        OrderFactory OrderFactory { get; }

        IDomainApprover DomainApprover { get; }

        public OrderCreator(
            OrderFactory orderFactory,
            IDomainApprover domainApprover) {

            OrderFactory = orderFactory;
            DomainApprover = domainApprover;
        }

        public OrderDetails Create(OrderCreatorArgs args) {

            var order = OrderFactory.Create(args.ToOrderFactoryArgs());

            DomainApprover.SaveChanges();

            return new OrderDetails {
                Id = order.OrderId.ToString()
            };
        }
    }
}
