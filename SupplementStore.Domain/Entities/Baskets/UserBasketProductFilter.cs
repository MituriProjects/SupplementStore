using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Entities.Baskets {

    public class UserBasketProductFilter : IFilter<BasketProduct> {

        string UserId { get; }

        Guid ProductId { get; }

        public UserBasketProductFilter(string userId, Guid productId) {

            UserId = userId;
            ProductId = productId;
        }

        public BasketProduct Process(IEnumerable<BasketProduct> entities) {

            return entities.FirstOrDefault(e => e.UserId == UserId && e.ProductId == ProductId);
        }
    }
}
