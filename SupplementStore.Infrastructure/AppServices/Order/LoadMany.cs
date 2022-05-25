using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Shared;
using SupplementStore.Infrastructure.ModelMapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Order {

    public partial class OrderService {

        public OrderListResult LoadMany(OrderListArgs args) {

            var orders = OrderRepository
                .FindBy(new PagingFilter<Domain.Orders.Order>(args.Skip, args.Take));

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(orders.Select(e => e.OrderId)));

            var products = ProductRepository.Entities
                .Where(e => ordersPurchases.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            var loadedOrders = new List<OrderDetails>();
            foreach (var order in orders) {

                var address = AddressRepository.FindBy(order.AddressId);

                loadedOrders.Add(order.ToDetails(address, ordersPurchases.ToDetails(products)));
            }

            return new OrderListResult {
                AllOrdersCount = OrderRepository.Count(),
                Orders = loadedOrders
            };
        }
    }
}
