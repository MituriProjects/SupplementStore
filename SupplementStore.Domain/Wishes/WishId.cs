using System;

namespace SupplementStore.Domain.Wishes {

    public class WishId : IdBase {

        public WishId(Guid id) : base(id) {
        }

        public WishId(string id) : base(id) {
        }
    }
}
