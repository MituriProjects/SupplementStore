using SupplementStore.Domain.Entities.Baskets;
using System;
using System.Linq;

namespace SupplementStore.Tests {

    class TestBasketProduct : BasketProduct {

        public TestBasketProduct WithUserId(string userId) {

            UserId = userId;

            return this;
        }

        public TestBasketProduct WithProductId(Guid productId) {

            ProductId = productId;

            return this;
        }

        public TestBasketProduct WithQuantity(int quantity) {

            Quantity = quantity;

            return this;
        }

        public static TestBasketProduct New() {

            var product = new TestBasketProduct();

            TestDocument<BasketProduct>.Add(product);

            return product;
        }

        public static TestBasketProduct Random() {

            var id = Guid.NewGuid();

            var product = new TestBasketProduct {
                Id = id,
                UserId = Guid.NewGuid().ToString(),
                ProductId = Guid.NewGuid(),
                Quantity = RandomManager.Next(10)
            };

            TestDocument<BasketProduct>.Add(product);

            return product;
        }

        public static TestBasketProduct[] Random(int count) =>
            new TestBasketProduct[count].Select(e => Random()).ToArray();
    }
}
