using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SupplementStore.Tests {

    public class ContentElement {

        string Value { get; }

        bool Occurs { get; }

        public ContentElement(object value, bool occurs) {

            Value = $"<div>{value.ToString()}</div>";
            Occurs = occurs;
        }

        public void Examine(string content) {

            if (Occurs) {

                if (content.Contains(Value) == false) {

                    ThrowNotFound(content);
                }
            }
            else {

                if (content.Contains(Value)) {

                    ThrowFound(content);
                }
            }
        }

        private void ThrowFound(string content) {

            throw new AssertFailedException($"Found: {Value}; Content: {content}");
        }

        private void ThrowNotFound(string content) {

            throw new AssertFailedException($"Not found: {Value}; Content: {content}");
        }
    }
}
