using SupplementStore.Application.Models;
using SupplementStore.Domain.Addresses;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class AddressExtensions {

        public static IEnumerable<AddressDetails> ToDetails(this IEnumerable<Address> addresses) {

            return addresses.Select(e => new AddressDetails {
                Id = e.AddressId.ToString(),
                Street = e.Street,
                PostalCode = e.PostalCode.Value,
                City = e.City
            }).ToList();
        }
    }
}
