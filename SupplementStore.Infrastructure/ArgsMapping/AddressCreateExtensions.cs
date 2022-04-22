using SupplementStore.Application.Args;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.ArgsMapping {

    static class AddressCreateExtensions {

        public static AddressFactoryArgs ToFactoryArgs(this AddressCreateArgs args) {

            return new AddressFactoryArgs {
                UserId = args.UserId,
                Street = args.Street,
                PostalCode = args.PostalCode,
                City = args.City
            };
        }
    }
}
