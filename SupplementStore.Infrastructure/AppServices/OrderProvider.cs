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

        IRepository<OrderProduct> OrderProductRepository { get; }

        IRepository<Product> ProductRepository { get; }

        public OrderProvider(
            IRepository<Order> orderRepository,
            IRepository<OrderProduct> orderProductRepository,
            IRepository<Product> productRepository) {

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

            var orderProducts = OrderProductRepository.FindBy(new OrderProductsFilter(order.Id));

            var products = ProductRepository.Entities
                .Where(e => orderProducts.Select(o => o.ProductId).Contains(e.Id))
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
                    ProductName = products.First(p => p.Id == e.ProductId).Name,
                    ProductPrice = products.First(p => p.Id == e.ProductId).Price,
                    Quantity = e.Quantity
                }).ToList()
            };
        }
    }
}
