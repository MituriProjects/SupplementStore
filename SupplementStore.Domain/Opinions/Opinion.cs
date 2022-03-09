using SupplementStore.Domain.Orders;
using System;

namespace SupplementStore.Domain.Opinions {

    public class Opinion : Entity {

        public OpinionId OpinionId { get; private set; } = new OpinionId(Guid.Empty);

        Guid OrderProduct_Id {
            get => OrderProductId.Id;
            set => OrderProductId = new OrderProductId(value);
        }

        public OrderProductId OrderProductId { get; set; }

        public string Text { get; set; }

        public Grade Grade { get; set; }
    }
}
