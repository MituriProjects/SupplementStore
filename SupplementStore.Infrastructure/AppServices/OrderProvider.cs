using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderProvider : IOrderProvider {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        public OrderProvider(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
        }

        public OrderDetails Load(string id) {

            var order = OrderRepository.FindBy(new OrderId(id));

            if (order == null)
                return null;

            var purchases = PurchaseRepository.FindBy(new OrderPurchasesFilter(order.OrderId));

            var products = ProductRepository.Entities
                .Where(e => purchases.Select(o => o.ProductId).Contains(e.ProductId))
                .ToList();

            return new OrderDetails {
                Id = order.OrderId.ToString(),
                UserId = order.UserId,
                Address = order.Address.Street,
                PostalCode = order.Address.PostalCode,
                City = order.Address.City,
                CreatedOn = order.CreatedOn,
                Purchases = purchases.Select(e => new PurchaseDetails {
                    ProductId = e.ProductId.ToString(),
                    ProductName = products.First(p => p.ProductId == e.ProductId).Name,
                    ProductPrice = products.First(p => p.ProductId == e.ProductId).Price,
                    Quantity = e.Quantity
                }).ToList()
            };
        }
    }
}
