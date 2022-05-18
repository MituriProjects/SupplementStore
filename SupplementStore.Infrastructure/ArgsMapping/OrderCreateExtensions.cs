using SupplementStore.Application.Args;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.ArgsMapping {

    static class OrderCreateExtensions {

        public static OrderFactoryArgs ToOrderFactoryArgs(this OrderCreateArgs orderCreatorArgs) {

            return new OrderFactoryArgs {
                UserId = orderCreatorArgs.UserId,
                Street = orderCreatorArgs.Street,
                PostalCode = orderCreatorArgs.PostalCode,
                City = orderCreatorArgs.City,
                ShouldAddressBeHidden = orderCreatorArgs.ShouldAddressBeHidden
            };
        }
    }
}
