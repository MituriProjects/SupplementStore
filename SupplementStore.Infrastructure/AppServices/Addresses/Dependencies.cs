using SupplementStore.Application.Services;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService : IAddressService {

        IAddressRepository AddressRepository { get; }

        AddressFactory AddressFactory { get; }

        IDomainApprover DomainApprover { get; }

        public AddressService(
            IAddressRepository addressRepository,
            AddressFactory addressFactory,
            IDomainApprover domainApprover) {

            AddressRepository = addressRepository;
            AddressFactory = addressFactory;
            DomainApprover = domainApprover;
        }
    }
}
