using System;

namespace SupplementStore.Domain.Products {

    public class ProductId : IdBase {

        private ProductId() : base("") {
        }

        public ProductId(Guid id) : base(id) {
        }

        public ProductId(string id) : base(id) {
        }
    }
}
