using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        IOrderRepository OrderRepository { get; }

        IRepository<BasketProduct> BasketProductRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IDocumentApprover DocumentApprover { get; }

        public OrderCreator(
            IOrderRepository orderRepository,
            IRepository<BasketProduct> basketProductRepository,
            IOrderProductRepository orderProductRepository,
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
                    OrderId = order.OrderId,
                    ProductId = basketProduct.ProductId,
                    Quantity = basketProduct.Quantity
                };

                OrderProductRepository.Add(orderProduct);

                BasketProductRepository.Delete(basketProduct.Id);
            }

            DocumentApprover.SaveChanges();

            return new OrderDetails {
                Id = order.Id.ToString()
            };
        }
    }
}
