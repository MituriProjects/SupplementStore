using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderProvider : IOrderProvider {

        IRepository<Order> OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        public OrderProvider(
            IRepository<Order> orderRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
        }

        public OrderDetails Load(string id) {

            if (Guid.TryParse(id, out var guidId) == false)
                return null;

            var order = OrderRepository.FindBy(guidId);

            if (order == null)
                return null;

            var orderProducts = OrderProductRepository.FindBy(new OrderProductsFilter(order.OrderId));

            var products = ProductRepository.Entities
                .Where(e => orderProducts.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            return new OrderDetails {
                Id = order.Id.ToString(),
                UserId = order.UserId,
                Address = order.Address.Street,
                PostalCode = order.Address.PostalCode,
                City = order.Address.City,
                CreatedOn = order.CreatedOn,
                OrderProducts = orderProducts.Select(e => new OrderProductDetails {
                    ProductId = e.ProductId.ToString(),
                    ProductName = products.First(p => p.ProductId == e.ProductId).Name,
                    ProductPrice = products.First(p => p.ProductId == e.ProductId).Price,
                    Quantity = e.Quantity
                }).ToList()
            };
        }
    }
}
