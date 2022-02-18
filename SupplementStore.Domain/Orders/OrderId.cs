using System;

namespace SupplementStore.Domain.Orders {

    public class OrderId : IdBase {

        public OrderId(Guid id) : base(id) {
        }

        public OrderId(string id) : base(id) {
        }
    }
}
