using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class WishRepository : Repository<Wish>, IWishRepository {

        public WishRepository(IDocument<Wish> document) : base(document) {
        }

        public void Delete(string userId, ProductId productId) {

            var wish = Document.All
                .FirstOrDefault(e => e.UserId == userId && e.ProductId == productId);

            if (wish == null)
                return;

            Document.Delete(wish);
        }
    }
}
