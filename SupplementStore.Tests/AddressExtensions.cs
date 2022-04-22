using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Tests {

    static class AddressExtensions {

        public static Address WithUserId(this Address address, IdentityUser user) {

            address.UserId = user.Id;

            return address;
        }

        public static Address WithStreet(this Address address, string street) {

            address.Street = street;

            return address;
        }

        public static Address WithPostalCode(this Address address, PostalCode postalCode) {

            address.PostalCode = postalCode;

            return address;
        }

        public static Address WithCity(this Address address, string city) {

            address.City = city;

            return address;
        }

        public static Address WithIsHidden(this Address address, bool isHidden) {

            address.IsHidden = isHidden;

            return address;
        }
    }
}
