using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Addresses {

    public class UserAddressesFilter : IManyFilter<Address> {

        string UserId { get; }

        public UserAddressesFilter(string userId) {

            UserId = userId;
        }

        public IEnumerable<Address> Process(IQueryable<Address> entities) {

            return entities.Where(e => e.UserId == UserId && e.IsHidden == false);
        }
    }
}
