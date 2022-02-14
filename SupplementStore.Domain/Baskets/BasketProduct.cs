using SupplementStore.Domain.Shared;
using System;

namespace SupplementStore.Domain.Baskets {

    public class BasketProduct : Entity {

        public BasketProductId BasketProductId { get; private set; } = new BasketProductId(Guid.Empty);

        Quantity ProductQuantity { get; set; }

        public string UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity {
            get => ProductQuantity.Value;
            set => ProductQuantity = new Quantity(value);
        }
    }
}
