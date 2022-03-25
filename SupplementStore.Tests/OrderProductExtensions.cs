using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class OrderProductExtensions {

        public static OrderProduct WithOrderId(this OrderProduct orderProduct, Order order) {

            orderProduct.OrderId = order.OrderId;

            return orderProduct;
        }

        public static OrderProduct WithProductId(this OrderProduct orderProduct, Product product) {

            orderProduct.ProductId = product.ProductId;

            return orderProduct;
        }

        public static OrderProduct WithOpinionId(this OrderProduct orderProduct, OpinionId opinionId) {

            orderProduct.OpinionId = opinionId;

            return orderProduct;
        }

        public static OrderProduct WithQuantity(this OrderProduct orderProduct, int quantity) {

            orderProduct.Quantity = quantity;

            return orderProduct;
        }
    }
}
