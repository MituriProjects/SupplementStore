using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class UserOrdersFilter : IManyFilter<Order> {

        string UserId { get; }

        public UserOrdersFilter(string userId) {

            UserId = userId;
        }

        public IEnumerable<Order> Process(IQueryable<Order> entities) {

            return entities.Where(e => e.UserId == UserId).ToList();
        }
    }
}
