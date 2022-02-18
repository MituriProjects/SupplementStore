using System;

namespace SupplementStore.Domain.Baskets {

    public class BasketProductId : IdBase {

        public BasketProductId() : base("") {
        }

        public BasketProductId(Guid id) : base(id) {
        }
    }
}
