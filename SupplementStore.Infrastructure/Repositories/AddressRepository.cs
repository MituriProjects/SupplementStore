using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.Repositories {

    public class AddressRepository : Repository<Address>, IAddressRepository {

        public AddressRepository(IDocument<Address> document) : base(document) {
        }
    }
}
