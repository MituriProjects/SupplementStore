using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductToOpineProvider : IProductToOpineProvider {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public ProductToOpineProvider(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public ProductToOpineResult Load(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(userOrders.Select(e => e.OrderId)));

            var purchase = ordersPurchases
                .FirstOrDefault(e => e.OpinionId == null);

            if (purchase == null)
                return ProductToOpineResult.Empty;

            var product = ProductRepository.FindBy(purchase.ProductId);

            var order = userOrders
                .First(e => e.OrderId == purchase.OrderId);

            return new ProductToOpineResult {
                PurchaseId = purchase.PurchaseId.ToString(),
                ProductName = product.Name,
                BuyingDate = order.CreatedOn
            };
        }
    }
}
