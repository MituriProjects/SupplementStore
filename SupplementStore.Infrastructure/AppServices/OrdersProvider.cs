using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrdersProvider : IOrdersProvider {

        IOrderRepository OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        public OrdersProvider(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
        }

        public IEnumerable<OrderDetails> Load() {

            var orders = OrderRepository.Entities.ToList();

            var ordersProducts = OrderProductRepository.FindBy(new OrdersProductsFilter(orders.Select(e => e.OrderId)));

            var products = ProductRepository.Entities
                .Where(e => ordersProducts.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            foreach (var order in orders) {

                yield return new OrderDetails {
                    Id = order.OrderId.ToString(),
                    UserId = order.UserId,
                    Address = order.Address.Street,
                    PostalCode = order.Address.PostalCode,
                    City = order.Address.City,
                    CreatedOn = order.CreatedOn,
                    OrderProducts = ordersProducts.Where(e => e.OrderId == order.OrderId).Select(e => new OrderProductDetails {
                        ProductId = e.ProductId.ToString(),
                        ProductName = products.First(p => p.ProductId == e.ProductId).Name,
                        ProductPrice = products.First(p => p.ProductId == e.ProductId).Price,
                        Quantity = e.Quantity
                    }).ToList()
                };
            }
        }
    }
}
