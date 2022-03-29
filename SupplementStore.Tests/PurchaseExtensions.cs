using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Tests {

    static class PurchaseExtensions {

        public static Purchase WithOrderId(this Purchase purchase, Order order) {

            purchase.OrderId = order.OrderId;

            return purchase;
        }

        public static Purchase WithProductId(this Purchase purchase, Product product) {

            purchase.ProductId = product.ProductId;

            return purchase;
        }

        public static Purchase WithOpinionId(this Purchase purchase, Opinion opinion) {

            purchase.OpinionId = opinion?.OpinionId;

            return purchase;
        }

        public static Purchase WithQuantity(this Purchase purchase, int quantity) {

            purchase.Quantity = quantity;

            return purchase;
        }
    }
}
