using SupplementStore.Domain.Entities.Orders;

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
    }
}
