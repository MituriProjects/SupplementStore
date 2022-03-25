using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OrdersProductsFilter : IManyFilter<OrderProduct> {

        IEnumerable<OrderId> OrderIds { get; }

        public OrdersProductsFilter(IEnumerable<OrderId> orderIds) {

            OrderIds = orderIds;
        }

        public IEnumerable<OrderProduct> Process(IQueryable<OrderProduct> entities) {

            return entities.Where(e => OrderIds.Contains(e.OrderId));
        }
    }
}
