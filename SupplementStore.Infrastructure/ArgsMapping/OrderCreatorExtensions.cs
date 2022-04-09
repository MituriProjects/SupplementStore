using SupplementStore.Application.Args;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.ArgsMapping {

    static class OrderCreatorExtensions {

        public static OrderFactoryArgs ToOrderFactoryArgs(this OrderCreatorArgs orderCreatorArgs) {

            return new OrderFactoryArgs {
                UserId = orderCreatorArgs.UserId,
                Address = orderCreatorArgs.Address,
                PostalCode = orderCreatorArgs.PostalCode,
                City = orderCreatorArgs.City
            };
        }
    }
}
