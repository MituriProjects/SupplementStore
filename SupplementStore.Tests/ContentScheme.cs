using System.Collections.Generic;

namespace SupplementStore.Tests {

    public class ContentScheme {

        List<ContentElement> Elements { get; } = new List<ContentElement>();

        public ContentScheme Contains(object value) {

            Elements.Add(new ContentElement(value, true));

            return this;
        }

        public ContentScheme Lacks(object value) {

            Elements.Add(new ContentElement(value, false));

            return this;
        }

        public void Examine(string content) {

            foreach (var contentElement in Elements) {

                contentElement.Examine(content);
            }
        }
    }
}
