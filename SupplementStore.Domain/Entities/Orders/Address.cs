using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Street,
                PostalCode,
                City
            };
        }

        private void Validate() {

            if (Regex.IsMatch(PostalCode, @"^\d{2}-\d{3}$") == false)
                throw new InvalidStateException(PostalCode);
        }
    }
}
