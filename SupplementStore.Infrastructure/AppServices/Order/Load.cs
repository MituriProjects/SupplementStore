using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using SupplementStore.Infrastructure.AppModels;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService {

        public OrderDetails Load(string id) {

            var order = OrderRepository.FindBy(new OrderId(id));

            if (order == null)
                return null;

            var purchases = PurchaseRepository.FindBy(new OrderPurchasesFilter(order.OrderId));

            var products = ProductRepository.Entities
                .Where(e => purchases.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            return OrderDetailsFactory.Create(order, PurchaseDetailsFactory.Create(purchases, products));
        }
    }
}
