using System;

namespace SupplementStore.Domain.Orders {

    public class OrderId : IdBase {

        public OrderId() : base("") {
        }

        public OrderId(Guid id) : base(id) {
        }
    }
}
