using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Orders;
using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderProvider : IOrderProvider {

        IDocument<Order> OrderDocument { get; }

        IDocument<OrderProduct> OrderProductDocument { get; }

        IDocument<Product> ProductDocument { get; }

        public OrderProvider(
            IDocument<Order> orderDocument,
            IDocument<OrderProduct> orderProductDocument,
            IDocument<Product> productDocument) {

            OrderDocument = orderDocument;
            OrderProductDocument = orderProductDocument;
            ProductDocument = productDocument;
        }

        public OrderDetails Load(string id) {

            var order = OrderDocument.All.FirstOrDefault(e => e.Id == Guid.Parse(id));

            if (order == null)
                return null;

            var orderProducts = OrderProductDocument.All
                .Where(e => e.OrderId == order.Id)
                .ToList();

            var products = ProductDocument.All
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
