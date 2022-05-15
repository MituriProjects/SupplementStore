using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class IsRequiredAttribute : RequiredAttribute {

        Type Type { get; }

        public IsRequiredAttribute(Type containingType = null, [CallerMemberName]string callerName = null) {

            Type = containingType;
            ErrorMessage = $"{callerName}RequiredErrorMessage";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var result = base.IsValid(value, validationContext);

            if (result != null)
                result.ErrorMessage = GetErrorMessage(validationContext);

            return result;
        }

        private string GetErrorMessage(ValidationContext validationContext) {

            var translatorType = Type == null
                ? typeof(Translator)
                : typeof(Translator<>).MakeGenericType(Type);

            dynamic translator = validationContext.GetService(translatorType);

            return translator.GetLocalizedText(ErrorMessage);
        }
    }
}
