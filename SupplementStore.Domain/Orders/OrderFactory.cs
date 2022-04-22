using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Baskets;

namespace SupplementStore.Domain.Orders {

    public class OrderFactory {

        IAddressRepository AddressRepository { get; }

        IOrderRepository OrderRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        public OrderFactory(
            IAddressRepository addressRepository,
            IOrderRepository orderRepository,
            IBasketProductRepository basketProductRepository,
            IPurchaseRepository purchaseRepository) {

            AddressRepository = addressRepository;
            OrderRepository = orderRepository;
            BasketProductRepository = basketProductRepository;
            PurchaseRepository = purchaseRepository;
        }

        public Order Create(OrderFactoryArgs args) {

            var address = ManageAddress(args);

            var order = CreateOrder(args.UserId, address.AddressId);

            CreatePurchases(args.UserId, order.OrderId);

            return order;
        }

        private Address ManageAddress(OrderFactoryArgs args) {

            var address = AddressRepository.FindBy(new UserWholeAddressFilter(args.UserId, args.Address, new PostalCode(args.PostalCode), args.City));

            if (address == null) {

                address = new Address {
                    UserId = args.UserId,
                    Street = args.Address,
                    PostalCode = new PostalCode(args.PostalCode),
                    City = args.City,
                    IsHidden = args.ShouldAddressBeHidden
                };

                AddressRepository.Add(address);
            }
            else if (address.IsHidden) {

                address.IsHidden = args.ShouldAddressBeHidden;
            }

            return address;
        }

        private Order CreateOrder(string userId, AddressId addressId) {

            var order = new Order {
                UserId = userId,
                AddressId = addressId
            };

            OrderRepository.Add(order);

            return order;
        }

        private void CreatePurchases(string userId, OrderId orderId) {

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(userId))) {

                var purchase = new Purchase {
                    OrderId = orderId,
                    ProductId = basketProduct.ProductId,
                    Quantity = basketProduct.Quantity
                };

                PurchaseRepository.Add(purchase);

                BasketProductRepository.Delete(basketProduct.BasketProductId);
            }
        }
    }
}
