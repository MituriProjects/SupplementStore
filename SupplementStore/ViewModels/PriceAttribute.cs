using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class PriceAttribute : WrapperValidationAttribute {

        public PriceAttribute([CallerFilePath]string filePath = null)
            : base(new RangeAttribute(0.01, double.MaxValue), "PriceAboveZeroErrorMessage", "Price above zero required.", filePath) {
        }
    }
}
