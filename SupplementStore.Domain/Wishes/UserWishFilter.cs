using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Domain.Wishes {

    public class UserWishFilter : IFilter<Wish> {

        string UserId { get; }

        ProductId ProductId { get; }

        public UserWishFilter(string userId, ProductId productId) {

            UserId = userId;
            ProductId = productId;
        }

        public Wish Process(IQueryable<Wish> entities) {

            return entities.FirstOrDefault(e => e.UserId == UserId && e.ProductId == ProductId);
        }
    }
}
