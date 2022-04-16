using SupplementStore.Application.Models;
using SupplementStore.Domain.Addresses;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class AddressExtensions {

        public static AddressDetails ToDetails(this Address address) {

            return new AddressDetails {
                Id = address.AddressId.ToString(),
                Street = address.Street,
                PostalCode = address.PostalCode.Value,
                City = address.City
            };
        }

        public static IEnumerable<AddressDetails> ToDetails(this IEnumerable<Address> addresses) {

            return addresses
                .Select(e => e.ToDetails())
                .ToList();
        }
    }
}
