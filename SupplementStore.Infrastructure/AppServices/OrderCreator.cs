using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        IRepository<Order> OrderRepository { get; }

        IRepository<BasketProduct> BasketProductRepository { get; }

        IRepository<OrderProduct> OrderProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public OrderCreator(
            IRepository<Order> orderRepository,
            IRepository<BasketProduct> basketProductRepository,
            IRepository<OrderProduct> orderProductRepository,
            IDocumentApprover documentApprover) {

            OrderRepository = orderRepository;
            BasketProductRepository = basketProductRepository;
            OrderProductRepository = orderProductRepository;
            DocumentApprover = documentApprover;
        }

        public OrderDetails Create(OrderCreatorArgs args) {

            var order = new Order {
                UserId = args.UserId,
                Address = new Address(args.Address, args.PostalCode, args.City)
            };

            OrderRepository.Add(order);

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(args.UserId))) {

                var orderProduct = new OrderProduct {
                    OrderId = order.Id,
                    ProductId = basketProduct.ProductId,
                    Quantity = basketProduct.Quantity
                };

                OrderProductRepository.Add(orderProduct);

                BasketProductRepository.Delete(basketProduct.Id);
            }

            if (order.GetBrokenRules().Count() == 0) {

                DocumentApprover.SaveChanges();

                return new OrderDetails {
                    Id = order.Id.ToString()
                };
            }

            return null;
        }
    }
}
