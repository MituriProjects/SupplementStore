using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class OrderProductExtensions {

        public static OrderProduct WithOrderId(this OrderProduct orderProduct, OrderId orderId) {

            orderProduct.OrderId = orderId;

            return orderProduct;
        }

        public static OrderProduct WithProductId(this OrderProduct orderProduct, ProductId productId) {

            orderProduct.ProductId = productId;

            return orderProduct;
        }

        public static OrderProduct WithQuantity(this OrderProduct orderProduct, int quantity) {

            orderProduct.Quantity = quantity;

            return orderProduct;
        }
    }
}
