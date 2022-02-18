using System;

namespace SupplementStore.Domain.Baskets {

    public class BasketProductId : IdBase {

        public BasketProductId() : base("") {
        }

        public BasketProductId(Guid id) : base(id) {
        }

        public BasketProductId(string id) : base(id) {
        }
    }
}
