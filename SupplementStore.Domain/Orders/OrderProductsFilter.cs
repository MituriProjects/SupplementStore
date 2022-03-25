using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OrderProductsFilter : IManyFilter<OrderProduct> {

        OrderId OrderId { get; }

        public OrderProductsFilter(OrderId orderId) {

            OrderId = orderId;
        }

        public IEnumerable<OrderProduct> Process(IQueryable<OrderProduct> entities) {

            return entities.Where(e => e.OrderId == OrderId);
        }
    }
}
