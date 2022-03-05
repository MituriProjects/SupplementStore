using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Wishes {

    public class UserWishesFilter : IManyFilter<Wish> {

        string UserId { get; }

        public UserWishesFilter(string userId) {

            UserId = userId;
        }

        public IEnumerable<Wish> Process(IQueryable<Wish> entities) {

            return entities.Where(e => e.UserId == UserId).ToList();
        }
    }
}
