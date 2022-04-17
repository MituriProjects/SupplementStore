using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IAddressService {
        AddressDetails Load(string addressId);
        IEnumerable<AddressDetails> LoadMany(string userId);
        void Create(AddressCreateArgs args);
        void Update(AddressUpdateArgs args);
    }
}
