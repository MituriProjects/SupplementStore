using System;

namespace SupplementStore.Domain.Orders {

    public class PurchaseId : IdBase {

        public PurchaseId(Guid id) : base(id) {
        }

        public PurchaseId(string id) : base(id) {
        }
    }
}
