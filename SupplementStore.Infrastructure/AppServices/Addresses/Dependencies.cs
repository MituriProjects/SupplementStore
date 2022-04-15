using SupplementStore.Application.Services;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService : IAddressService {

        AddressFactory AddressFactory { get; }

        IDomainApprover DomainApprover { get; }

        public AddressService(
            AddressFactory addressFactory,
            IDomainApprover domainApprover) {

            AddressFactory = addressFactory;
            DomainApprover = domainApprover;
        }
    }
}
