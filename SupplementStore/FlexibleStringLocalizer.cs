using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SupplementStore {

    public class FlexibleStringLocalizer<T> : FlexibleStringLocalizer, IStringLocalizer<T> {

        public FlexibleStringLocalizer(IStringLocalizerFactory factory)
            : base(typeof(T), factory) {
        }
    }

    public class FlexibleStringLocalizer : IStringLocalizer {

        IStringLocalizer DedicatedResourceLocalizer { get; set; }

        IStringLocalizer SharedResourceLocalizer { get; set; }

        public FlexibleStringLocalizer(Type type, IStringLocalizerFactory factory) {

            DedicatedResourceLocalizer = factory.Create(type);
            SharedResourceLocalizer = factory.Create(typeof(SharedResource));
        }

        public LocalizedString this[string name] {

            get {

                var localizedString = DedicatedResourceLocalizer[name];

                if (localizedString.ResourceNotFound)
                    localizedString = SharedResourceLocalizer[name];

                return localizedString;
            }
        }

        public LocalizedString this[string name, params object[] arguments] {

            get {

                var localizedString = DedicatedResourceLocalizer[name, arguments];

                if (localizedString.ResourceNotFound)
                    localizedString = SharedResourceLocalizer[name, arguments];

                return localizedString;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) {

            return DedicatedResourceLocalizer
                .GetAllStrings(includeParentCultures)
                .Concat(SharedResourceLocalizer.GetAllStrings(includeParentCultures));
        }

        public IStringLocalizer WithCulture(CultureInfo culture) {

            DedicatedResourceLocalizer = DedicatedResourceLocalizer.WithCulture(culture);
            SharedResourceLocalizer = SharedResourceLocalizer.WithCulture(culture);

            return this;
        }
    }
}
