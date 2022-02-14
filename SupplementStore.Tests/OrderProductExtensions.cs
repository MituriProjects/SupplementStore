﻿using SupplementStore.Domain.Orders;
using System;

namespace SupplementStore.Tests {

    static class OrderProductExtensions {

        public static OrderProduct WithOrderId(this OrderProduct orderProduct, Guid orderId) {

            orderProduct.OrderId = orderId;

            return orderProduct;
        }

        public static OrderProduct WithProductId(this OrderProduct orderProduct, Guid productId) {

            orderProduct.ProductId = productId;

            return orderProduct;
        }

        public static OrderProduct WithQuantity(this OrderProduct orderProduct, int quantity) {

            orderProduct.Quantity = quantity;

            return orderProduct;
        }
    }
}
