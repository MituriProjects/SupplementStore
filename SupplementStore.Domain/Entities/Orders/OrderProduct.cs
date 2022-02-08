using System;

namespace SupplementStore.Domain.Entities.Orders {

    public class OrderProduct : Entity {

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        protected override void Validate() {

            if (Quantity <= 0)
                AddBrokenRule(OrderProductBusinessRules.QuantityAboveZeroRequired);
        }
    }
}
