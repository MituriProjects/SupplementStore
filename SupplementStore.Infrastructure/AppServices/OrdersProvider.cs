using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.AppModels;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrdersProvider : IOrdersProvider {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        public OrdersProvider(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
        }

        public IEnumerable<OrderDetails> Load() {

            var orders = OrderRepository.Entities.ToList();

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(orders.Select(e => e.OrderId)));

            var products = ProductRepository.Entities
                .Where(e => ordersPurchases.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            foreach (var order in orders) {

                yield return OrderDetailsFactory.Create(order, PurchaseDetailsFactory.Create(ordersPurchases, products));
            }
        }
    }
}
