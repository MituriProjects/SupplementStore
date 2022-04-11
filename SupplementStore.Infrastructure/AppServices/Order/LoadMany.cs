using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using SupplementStore.Infrastructure.ModelMapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService {

        public IEnumerable<OrderDetails> LoadMany() {

            var orders = OrderRepository.Entities.ToList();

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(orders.Select(e => e.OrderId)));

            var products = ProductRepository.Entities
                .Where(e => ordersPurchases.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            foreach (var order in orders) {

                yield return order.ToDetails(ordersPurchases.ToDetails(products));
            }
        }
    }
}
