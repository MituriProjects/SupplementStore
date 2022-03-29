using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OrdersPurchasesFilter : IManyFilter<Purchase> {

        IEnumerable<OrderId> OrderIds { get; }

        public OrdersPurchasesFilter(IEnumerable<OrderId> orderIds) {

            OrderIds = orderIds;
        }

        public IEnumerable<Purchase> Process(IQueryable<Purchase> entities) {

            return entities.Where(e => OrderIds.Contains(e.OrderId));
        }
    }
}
