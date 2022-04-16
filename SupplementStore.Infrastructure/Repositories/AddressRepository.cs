using SupplementStore.Domain.Addresses;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class AddressRepository : Repository<Address>, IAddressRepository {

        public AddressRepository(IDocument<Address> document) : base(document) {
        }

        public Address FindBy(AddressId addressId) {

            return Document.All
                .FirstOrDefault(e => e.AddressId == addressId);
        }
    }
}
