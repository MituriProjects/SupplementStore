using SupplementStore.Domain;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Tests {

    static class PostalCodeFactory {

        public static PostalCode Create(Entity entity) {

            var value = $"{RandomManager.Next(100).ToString("00")}-{RandomManager.Next(1000).ToString("000")}";

            return new PostalCode(value);
        }
    }
}
