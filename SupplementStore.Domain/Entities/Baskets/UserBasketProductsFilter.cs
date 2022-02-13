using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Entities.Baskets {

    public class UserBasketProductsFilter : IManyFilter<BasketProduct> {

        string UserId { get; }

        public UserBasketProductsFilter(string userId) {

            UserId = userId;
        }

        public IEnumerable<BasketProduct> Process(IQueryable<BasketProduct> entities) {

            return entities.Where(e => e.UserId == UserId).ToList();
        }
    }
}
