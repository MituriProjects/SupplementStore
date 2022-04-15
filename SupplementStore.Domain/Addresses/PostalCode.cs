using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SupplementStore.Domain.Addresses {

    public class PostalCode : ValueObject<PostalCode> {

        public string Value { get; private set; }

        public PostalCode() {
        }

        public PostalCode(string value) {

            Value = value;

            Validate();
        }

        protected override IEnumerable<object> GetValues() {

            yield return Value;
        }

        private void Validate() {

            if (Regex.IsMatch(Value, @"^\d{2}-\d{3}$") == false)
                throw new InvalidStateException($"Invalid postal code format. Required: 00-000; Actual: {Value};");
        }
    }
}
