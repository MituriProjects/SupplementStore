using Microsoft.Extensions.Localization;

namespace SupplementStore {

    public class Translator {

        IStringLocalizer<SharedResource> Localizer { get; }

        public Translator(IStringLocalizer<SharedResource> localizer) {

            Localizer = localizer;
        }

        public string GetLocalizedText(string key) {

            return Localizer[key];
        }
    }
}
