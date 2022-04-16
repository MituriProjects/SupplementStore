using SupplementStore.Application.Models;
using SupplementStore.Domain.Addresses;
using SupplementStore.Infrastructure.ModelMapping;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService {

        public IEnumerable<AddressDetails> LoadMany(string userId) {

            return AddressRepository
                .FindBy(new UserAddressesFilter(userId))
                .ToDetails();
        }
    }
}
