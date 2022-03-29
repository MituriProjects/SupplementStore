using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Shared;
using System;

namespace SupplementStore.Domain.Orders {

    public class Purchase : Entity {

        public PurchaseId PurchaseId { get; private set; } = new PurchaseId(Guid.Empty);

        Quantity ProductQuantity { get; set; }

        Guid Order_Id {
            get => OrderId.Id;
            set => OrderId = new OrderId(value);
        }

        public OrderId OrderId { get; set; }

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }

        Guid? Opinion_Id {
            get => OpinionId?.Id;
            set => OpinionId = value.HasValue ? new OpinionId(value.Value) : null;
        }

        public OpinionId OpinionId { get; set; }

        public int Quantity {
            get => ProductQuantity.Value;
            set => ProductQuantity = new Quantity(value);
        }
    }
}
