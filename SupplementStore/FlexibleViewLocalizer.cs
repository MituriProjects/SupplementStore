using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace SupplementStore {

    public class FlexibleViewLocalizer : ViewLocalizer {

        IStringLocalizer<SharedResource> SharedResourceLocalizer { get; }

        public FlexibleViewLocalizer(
            IHtmlLocalizerFactory localizerFactory,
            IHostingEnvironment hostingEnvironment,
            IStringLocalizer<SharedResource> sharedResourceLocalizer)
            : base(localizerFactory, hostingEnvironment) {

            SharedResourceLocalizer = sharedResourceLocalizer;
        }

        public override LocalizedHtmlString this[string key] {

            get {

                LocalizedHtmlString localizedHtmlString = base[key];

                if (localizedHtmlString.IsResourceNotFound) {

                    LocalizedString localizedString = SharedResourceLocalizer[key];

                    localizedHtmlString = new LocalizedHtmlString(localizedString.Name, localizedString.Value);
                }

                return localizedHtmlString;
            }
        }
    }
}
