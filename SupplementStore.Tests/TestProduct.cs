using SupplementStore.Domain.Entities.Products;

namespace SupplementStore.Tests {

    class TestProduct : Product {

        public TestProduct WithName(string name) {

            Name = name;

            return this;
        }

        public TestProduct WithPrice(decimal price) {

            Price = price;

            return this;
        }
    }
}
