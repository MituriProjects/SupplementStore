using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Entities.Orders {

    public class OrderProductsFilter : IManyFilter<OrderProduct> {

        Guid OrderId { get; }

        public OrderProductsFilter(Guid orderId) {

            OrderId = orderId;
        }

        public IEnumerable<OrderProduct> Process(IQueryable<OrderProduct> entities) {

            return entities.Where(e => e.OrderId == OrderId).ToList();
        }
    }
}
