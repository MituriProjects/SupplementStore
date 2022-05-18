using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class EmailAttribute : WrapperValidationAttribute {

        public EmailAttribute([CallerFilePath]string filePath = null)
            : base(new EmailAddressAttribute(), "EmailErrorMessage", "Invalid e-mail.", filePath) {
        }
    }
}
