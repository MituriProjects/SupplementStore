using SupplementStore.Domain.Shared;
using System;

namespace SupplementStore.Domain.Orders {

    public class OrderProduct : Entity {

        Quantity ProductQuantity { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity {
            get => ProductQuantity.Value;
            set => ProductQuantity = new Quantity(value);
        }
    }
}
