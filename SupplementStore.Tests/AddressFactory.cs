using SupplementStore.Domain;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Tests {

    static class AddressFactory {

        public static Address Create(Entity entity) {

            var street = $"{entity.GetType().Name}-Address-Street-{entity.GetId()}";
            var postalCode = $"{RandomManager.Next(100).ToString("00")}-{RandomManager.Next(1000).ToString("000")}";
            var city = $"{entity.GetType().Name}-Address-City-{entity.GetId()}";

            return new Address(street, postalCode, city);
        }
    }
}
