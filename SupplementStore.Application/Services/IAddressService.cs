using SupplementStore.Application.Args;

namespace SupplementStore.Application.Services {

    public interface IAddressService {
        void Create(AddressCreateArgs args);
    }
}
