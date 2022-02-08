using SupplementStore.Domain.Entities.Orders;
using System;
using System.Linq;

namespace SupplementStore.Tests {

    class TestOrder : Order {

        public TestOrder WithUserId(string userId) {

            UserId = userId;

            return this;
        }

        public TestOrder WithAddress(string address) {

            Address = address;

            return this;
        }

        public TestOrder WithPostalCode(string postalCode) {

            PostalCode = postalCode;

            return this;
        }

        public TestOrder WithCity(string city) {

            City = city;

            return this;
        }

        public static TestOrder New() {

            var order = new TestOrder();

            TestDocument<Order>.Add(order);

            return order;
        }

        public static TestOrder Random() {

            var id = Guid.NewGuid();

            var order = new TestOrder {
                Id = id,
                Address = $"TestOrderAddress-{id}",
                PostalCode = $"TestOrderPostalCode-{id}",
                City = $"TestOrderCity-{id}"
            };

            TestDocument<Order>.Add(order);

            return order;
        }

        public static TestOrder[] Random(int count) =>
            new TestOrder[count].Select(e => Random()).ToArray();
    }
}
