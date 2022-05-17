using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class IsRequiredAttribute : WrapperValidationAttribute {

        public IsRequiredAttribute([CallerFilePath]string filePath = null, [CallerMemberName]string callerName = null)
            : base(new RequiredAttribute(), $"{callerName}RequiredErrorMessage", $"Required {callerName}.", filePath) {
        }
    }
}
