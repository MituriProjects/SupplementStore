using System;

namespace SupplementStore.Domain.Products {

    public class ProductId : IdBase {

        public ProductId(Guid id) : base(id) {
        }

        public ProductId(string id) : base(id) {
        }
    }
}
