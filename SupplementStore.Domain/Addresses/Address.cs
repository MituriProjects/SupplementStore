using System;

namespace SupplementStore.Domain.Addresses {

    public class Address : UserEntity {

        public AddressId AddressId { get; private set; } = new AddressId(Guid.Empty);

        public string Street { get; set; }

        public PostalCode PostalCode { get; set; }

        public string City { get; set; }

        public bool IsHidden { get; set; }
    }
}
