using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OrderPurchasesFilter : IManyFilter<Purchase> {

        OrderId OrderId { get; }

        public OrderPurchasesFilter(OrderId orderId) {

            OrderId = orderId;
        }

        public IEnumerable<Purchase> Process(IQueryable<Purchase> entities) {

            return entities.Where(e => e.OrderId == OrderId);
        }
    }
}
