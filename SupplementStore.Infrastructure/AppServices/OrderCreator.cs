using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        IOrderRepository OrderRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OrderCreator(
            IOrderRepository orderRepository,
            IBasketProductRepository basketProductRepository,
            IOrderProductRepository orderProductRepository,
            IDomainApprover domainApprover) {

            OrderRepository = orderRepository;
            BasketProductRepository = basketProductRepository;
            OrderProductRepository = orderProductRepository;
            DomainApprover = domainApprover;
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

                BasketProductRepository.Delete(basketProduct.BasketProductId);
            }

            DomainApprover.SaveChanges();

            return new OrderDetails {
                Id = order.OrderId.ToString()
            };
        }
    }
}
