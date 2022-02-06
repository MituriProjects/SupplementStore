using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace SupplementStore.Tests {

    public class ContentElement {

        string Value { get; }

        bool Occurs { get; }

        private ContentElement(string value, bool occurs) {

            Value = value;
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

        public static ContentElement Html(string name, object value, bool occurs) {

            return new ContentElement($"<div name=\"{name}\">{value.ToString()}</div>", occurs);
        }

        public static ContentElement Json(string name, object value, bool occurs) {

            return new ContentElement($"\"{name}\":{JsonConvert.ToString(value)}", occurs);
        }
    }
}
