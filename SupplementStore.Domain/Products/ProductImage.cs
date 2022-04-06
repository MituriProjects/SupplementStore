using System;

namespace SupplementStore.Domain.Products {

    public class ProductImage : Entity {

        public ProductImageId ProductImageId { get; private set; } = new ProductImageId(Guid.Empty);

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }
    }
}
