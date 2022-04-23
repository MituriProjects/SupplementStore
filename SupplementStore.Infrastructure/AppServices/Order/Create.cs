using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Infrastructure.ArgsMapping;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService {

        public OrderDetails Create(OrderCreateArgs args) {

            var order = OrderFactory.Create(args.ToOrderFactoryArgs());

            DomainApprover.SaveChanges();

            return order.ToDetails();
        }
    }
}
