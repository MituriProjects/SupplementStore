using SupplementStore.Domain.Orders;
using System;

namespace SupplementStore.Domain.Opinions {

    public class Opinion : Entity {

        public OpinionId OpinionId { get; private set; } = new OpinionId(Guid.Empty);

        Guid Purchase_Id {
            get => PurchaseId.Id;
            set => PurchaseId = new PurchaseId(value);
        }

        public PurchaseId PurchaseId { get; set; }

        public string Text { get; set; }

        public Rating Rating { get; set; }

        public bool IsHidden { get; set; }
    }
}
