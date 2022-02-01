using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

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

        public static TestProduct New() {

            var product = new TestProduct();

            TestDocument<Product>.Add(product);

            return product;
        }

        public static TestProduct Random() {

            var id = Guid.NewGuid();

            var product = new TestProduct {
                Id = id,
                Name = $"TestProduct-{id}",
                Price = RandomManager.Next(10000) / 100M
            };

            TestDocument<Product>.Add(product);

            return product;
        }

        public static TestProduct[] Random(int count) =>
            new TestProduct[count].Select(e => Random()).ToArray();
    }
}
