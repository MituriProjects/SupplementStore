using SupplementStore.Domain.Baskets;

namespace SupplementStore.Domain.Orders {

    public class OrderFactory {

        IOrderRepository OrderRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        public OrderFactory(
            IOrderRepository orderRepository,
            IBasketProductRepository basketProductRepository,
            IPurchaseRepository purchaseRepository) {

            OrderRepository = orderRepository;
            BasketProductRepository = basketProductRepository;
            PurchaseRepository = purchaseRepository;
        }

        public Order Create(OrderFactoryArgs args) {

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

            return order;
        }
    }
}
