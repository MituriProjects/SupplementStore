using System;

namespace SupplementStore.Domain.Products {

    public class ProductImageId : IdBase {

        public ProductImageId(Guid id) : base(id) {
        }

        public ProductImageId(string id) : base(id) {
        }
    }
}
