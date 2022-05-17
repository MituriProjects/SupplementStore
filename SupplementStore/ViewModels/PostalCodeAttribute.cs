using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class PostalCodeAttribute : WrapperValidationAttribute {

        public PostalCodeAttribute([CallerFilePath]string filePath = null)
            : base(new RegularExpressionAttribute(@"^\d{2}-\d{3}$"), "InvalidPostalCodeErrorMessage", "Invalid postal code.", filePath) {
        }
    }
}
