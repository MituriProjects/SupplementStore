using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using SupplementStore.Infrastructure.ModelMapping;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public IEnumerable<OpinionDetails> LoadMany(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(userOrders.Select(e => e.OrderId)));

            var opinions = OpinionRepository.FindBy(ordersPurchases.Select(e => e.OpinionId));

            foreach (var opinion in opinions) {

                var purchase = ordersPurchases
                    .First(e => e.OpinionId == opinion.OpinionId);

                var product = ProductRepository.FindBy(purchase.ProductId);

                var order = userOrders.First(e => e.OrderId == purchase.OrderId);

                yield return opinion.ToDetails(product, order);
            }
        }
    }
}
