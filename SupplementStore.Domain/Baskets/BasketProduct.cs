using SupplementStore.Domain.Products;
using SupplementStore.Domain.Shared;
using System;

namespace SupplementStore.Domain.Baskets {

    public class BasketProduct : UserEntity {

        public BasketProductId BasketProductId { get; private set; } = new BasketProductId(Guid.Empty);

        Quantity ProductQuantity { get; set; }

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }

        public int Quantity {
            get => ProductQuantity.Value;
            set => ProductQuantity = new Quantity(value);
        }
    }
}
