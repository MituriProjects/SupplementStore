using System.Collections.Generic;

namespace SupplementStore.Domain.Products {

    public class ProductPrice : ValueObject<ProductPrice> {

        public decimal Value { get; private set; }

        public ProductPrice(decimal value) {

            Value = value;

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Value
            };
        }

        private void Validate() {

            if (Value <= 0)
                throw new InvalidStateException($"A product's price has to be above zero. Current value: {Value};");
        }
    }
}
