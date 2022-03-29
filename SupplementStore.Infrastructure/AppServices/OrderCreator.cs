using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.AppServices {

    public class OrderCreator : IOrderCreator {

        IOrderRepository OrderRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IDomainApprover DomainApprover { get; }

        public OrderCreator(
            IOrderRepository orderRepository,
            IBasketProductRepository basketProductRepository,
            IPurchaseRepository purchaseRepository,
            IDomainApprover domainApprover) {

            OrderRepository = orderRepository;
            BasketProductRepository = basketProductRepository;
            PurchaseRepository = purchaseRepository;
            DomainApprover = domainApprover;
        }

        public OrderDetails Create(OrderCreatorArgs args) {

            var order = new Order {
                UserId = args.UserId,
                Address = new Address(args.Address, args.PostalCode, args.City)
            };

            OrderRepository.Add(order);

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(args.UserId))) {

                var purchase = new Purchase {
                    OrderId = order.OrderId,
                    ProductId = basketProduct.ProductId,
                    Quantity = basketProduct.Quantity
                };

                PurchaseRepository.Add(purchase);

                BasketProductRepository.Delete(basketProduct.BasketProductId);
            }

            DomainApprover.SaveChanges();

            return new OrderDetails {
                Id = order.OrderId.ToString()
            };
        }
    }
}
