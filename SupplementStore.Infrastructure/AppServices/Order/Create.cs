using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Infrastructure.ArgsMapping;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService {

        public OrderDetails Create(OrderCreatorArgs args) {

            var order = OrderFactory.Create(args.ToOrderFactoryArgs());

            DomainApprover.SaveChanges();

            return new OrderDetails {
                Id = order.OrderId.ToString()
            };
        }
    }
}
