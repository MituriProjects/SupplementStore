using System.Collections.Generic;

namespace SupplementStore.Domain.Shared {

    public class Quantity : ValueObject<Quantity> {

        public int Value { get; private set; }

        public Quantity(int value) {

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
                throw new InvalidStateException($"Quantity has to be above zero. Current value: {Value};");
        }
    }
}
