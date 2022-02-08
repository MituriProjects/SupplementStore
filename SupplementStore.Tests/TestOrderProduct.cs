using SupplementStore.Domain.Entities.Orders;
using System;

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
    }
}
