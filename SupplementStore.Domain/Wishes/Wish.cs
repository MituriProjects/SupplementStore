using SupplementStore.Domain.Products;
using System;

namespace SupplementStore.Domain.Wishes {

    public class Wish : UserEntity {

        WishId WishId { get; set; } = new WishId(Guid.Empty);

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }
    }
}
