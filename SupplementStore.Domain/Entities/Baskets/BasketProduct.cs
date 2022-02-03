using System;

namespace SupplementStore.Domain.Entities.Baskets {

    public class BasketProduct : Entity {

        public string UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        protected override void Validate() {

            if (Quantity <= 0)
                AddBrokenRule(BasketProductBusinessRules.QuantityAboveZeroRequired);
        }
    }
}
