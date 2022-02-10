using System.Collections.Generic;

namespace SupplementStore.Domain.Entities.Orders {

    public class Address : ValueObject<Address> {

        public string Street { get; private set; }

        public string PostalCode { get; private set; }

        public string City { get; private set; }

        private Address() {
        }

        public Address(string street, string postalCode, string city) {

            Street = street;
            PostalCode = postalCode;
            City = city;
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Street,
                PostalCode,
                City
            };
        }
    }
}
