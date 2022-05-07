using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels {

    public class EmailAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            return new EmailAddressAttribute().IsValid(value)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage(validationContext));
        }

        private string GetErrorMessage(ValidationContext validationContext) {

            Translator translation = validationContext.GetService(typeof(Translator)) as Translator;

            var errorMessageKey = "EmailErrorMessage";
            var errorMessage = translation.GetLocalizedText(errorMessageKey);

            return errorMessage == errorMessageKey
                ? "Invalid e-mail."
                : errorMessage;
        }
    }
}
