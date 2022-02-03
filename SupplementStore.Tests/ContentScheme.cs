using System.Collections.Generic;

namespace SupplementStore.Tests {

    public class ContentScheme {

        List<ContentElement> Elements { get; } = new List<ContentElement>();

        public ContentScheme Contains(string name, object value) {

            Elements.Add(new ContentElement(name, value, true));

            return this;
        }

        public ContentScheme Lacks(string name, object value) {

            Elements.Add(new ContentElement(name, value, false));

            return this;
        }

        public void Examine(string content) {

            foreach (var contentElement in Elements) {

                contentElement.Examine(content);
            }
        }
    }
}
