using SupplementStore.Domain.Entities.Baskets;
using System;

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
    }
}
