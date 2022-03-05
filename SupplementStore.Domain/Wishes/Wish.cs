using SupplementStore.Domain.Products;
using System;

namespace SupplementStore.Domain.Wishes {

    public class Wish : Entity {

        WishId WishId { get; set; } = new WishId(Guid.Empty);

        public string UserId { get; set; }

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }
    }
}
