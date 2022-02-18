using System.Collections.Generic;

namespace SupplementStore.Domain.Products {

    public class ProductName : ValueObject<ProductName> {

        public string Value { get; private set; }

        public ProductName(string value) {

            Value = value;

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Value
            };
        }

        private void Validate() {

            if (string.IsNullOrEmpty(Value))
                throw new InvalidStateException("ProductName requires an injected name to have a value.");
        }
    }
}
