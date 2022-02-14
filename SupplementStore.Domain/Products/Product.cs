using System;

namespace SupplementStore.Domain.Products {

    public class Product : Entity {

        public ProductId ProductId { get; private set; } = new ProductId(Guid.Empty);

        ProductName ProductName { get; set; }

        ProductPrice ProductPrice { get; set; }

        public string Name {
            get => ProductName.Value;
            set => ProductName = new ProductName(value);
        }

        public decimal Price {
            get => ProductPrice.Value;
            set => ProductPrice = new ProductPrice(value);
        }
    }
}
