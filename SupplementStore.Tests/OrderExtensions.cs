using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class OrderExtensions {

        public static Order WithUserId(this Order order, string userId) {

            order.UserId = userId;

            return order;
        }

        public static Order WithAddress(this Order order, Address address) {

            order.Address = address;

            return order;
        }
    }
}
