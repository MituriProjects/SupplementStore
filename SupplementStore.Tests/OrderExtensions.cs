using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class OrderExtensions {

        public static Order WithUserId(this Order order, IdentityUser user) {

            order.UserId = user.Id;

            return order;
        }

        public static Order WithAddressId(this Order order, Address address) {

            order.AddressId = address.AddressId;

            return order;
        }
    }
}
