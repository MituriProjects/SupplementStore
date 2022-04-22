using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class OrderExtensions {

        public static Order WithUserId(this Order order, string userId) {

            order.UserId = userId;

            return order;
        }

        public static Order WithAddressId(this Order order, Address address) {

            order.AddressId = address.AddressId;

            return order;
        }
    }
}
