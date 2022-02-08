using SupplementStore.Domain.Entities.Orders;
using System;
using System.Linq;

namespace SupplementStore.Tests {

    class TestOrderProduct : OrderProduct {

        public TestOrderProduct WithOrderId(Guid orderId) {

            OrderId = orderId;

            return this;
        }

        public TestOrderProduct WithProductId(Guid productId) {

            ProductId = productId;

            return this;
        }

        public TestOrderProduct WithQuantity(int quantity) {

            Quantity = quantity;

            return this;
        }

        public static TestOrderProduct New() {

            var orderProduct = new TestOrderProduct();

            TestDocument<OrderProduct>.Add(orderProduct);

            return orderProduct;
        }

        public static TestOrderProduct Random() {

            var id = Guid.NewGuid();

            var orderProduct = new TestOrderProduct {
                Id = id,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = RandomManager.Next(10)
            };

            TestDocument<OrderProduct>.Add(orderProduct);

            return orderProduct;
        }

        public static TestOrderProduct[] Random(int count) =>
            new TestOrderProduct[count].Select(e => Random()).ToArray();
    }
}
