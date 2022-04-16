using SupplementStore.Application.Models;
using SupplementStore.Domain.Addresses;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService {

        public AddressDetails Load(string addressId) {

            return AddressRepository
                .FindBy(new AddressId(addressId))?
                .ToDetails();
        }
    }
}
