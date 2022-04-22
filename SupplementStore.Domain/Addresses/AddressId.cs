using System;

namespace SupplementStore.Domain.Addresses {

    public class AddressId : IdBase {

        public AddressId(Guid id) : base(id) {
        }

        public AddressId(string id) : base(id) {
        }
    }
}
