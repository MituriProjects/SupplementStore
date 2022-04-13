using SupplementStore.Application.Results;
using SupplementStore.Domain.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public ProductToOpineResult LoadProductToOpine(string userId) {

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
