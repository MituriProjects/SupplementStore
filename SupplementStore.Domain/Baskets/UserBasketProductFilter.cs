using System;
using System.Linq;

namespace SupplementStore.Domain.Baskets {

    public class UserBasketProductFilter : IFilter<BasketProduct> {

        string UserId { get; }

        Guid ProductId { get; }

        public UserBasketProductFilter(string userId, Guid productId) {

            UserId = userId;
            ProductId = productId;
        }

        public BasketProduct Process(IQueryable<BasketProduct> entities) {

            return entities.FirstOrDefault(e => e.UserId == UserId && e.ProductId == ProductId);
        }
    }
}
