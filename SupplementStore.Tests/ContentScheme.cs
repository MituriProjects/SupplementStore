using System;
using System.Collections.Generic;

namespace SupplementStore.Tests {

    public class ContentScheme {

        List<ContentElement> Elements { get; } = new List<ContentElement>();

        Func<string, object, bool, ContentElement> ContentElementFactory { get; set; }

        private ContentScheme(Func<string, object, bool, ContentElement> contentElementFactory) {

            ContentElementFactory = contentElementFactory;
        }

        public ContentScheme Contains(string name, object value) {

            Elements.Add(ContentElementFactory(name, value, true));

            return this;
        }

        public ContentScheme Lacks(string name, object value) {

            Elements.Add(ContentElementFactory(name, value, false));

            return this;
        }

        public void Examine(string content) {

            foreach (var contentElement in Elements) {

                contentElement.Examine(content);
            }
        }

        public static ContentScheme Json() {

            return new ContentScheme((s, o, b) => {

                s = char.ToLower(s[0]) + s.Substring(1);

                return ContentElement.Json(s, o, b);
            });

        }

        public static ContentScheme Html() {

            return new ContentScheme((s, o, b) => ContentElement.Html(s, o, b));
        }
    }
}
