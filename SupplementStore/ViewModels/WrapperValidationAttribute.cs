using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SupplementStore.ViewModels {

    public class WrapperValidationAttribute : ValidationAttribute {

        ValidationAttribute InnerValidationAttribute { get; }

        string ErrorMessageLocalizationKey { get; }

        string FallbackErrorMessage { get; }

        Type Type { get; set; }

        public WrapperValidationAttribute(ValidationAttribute innerValidationAttribute, string errorMessageLocalizationKey, string fallbackErrorMessage, string filePath = null) {

            InnerValidationAttribute = innerValidationAttribute;

            ErrorMessageLocalizationKey = errorMessageLocalizationKey;
            FallbackErrorMessage = fallbackErrorMessage;

            DetermineType(filePath);
        }

        void DetermineType(string filePath) {

            if (filePath == null)
                return;

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            var viewModelsPathPiece = assemblyName + "\\ViewModels";

            var typeFullName = filePath
                .Substring(filePath.IndexOf(viewModelsPathPiece), filePath.LastIndexOf(".") - filePath.IndexOf(viewModelsPathPiece))
                .Replace("\\", ".");

            Type = assembly.GetType(typeFullName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            return InnerValidationAttribute.IsValid(value)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage(validationContext));
        }

        string GetErrorMessage(ValidationContext validationContext) {

            var localizer = validationContext.GetService(typeof(IStringLocalizer<>).MakeGenericType(Type)) as IStringLocalizer;

            var localizedErrorMessage = localizer[ErrorMessageLocalizationKey];

            return localizedErrorMessage.ResourceNotFound
                ? FallbackErrorMessage
                : localizedErrorMessage;
        }
    }
}
