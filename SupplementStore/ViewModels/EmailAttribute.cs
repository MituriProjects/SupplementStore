using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class EmailAttribute : ValidationAttribute {

        Type Type { get; }

        public EmailAttribute([CallerFilePath]string filePath = null) {

            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetName()
                .Name;

            var viewModelsPathPiece = assemblyName + "\\ViewModels";

            var typeFullName = filePath
                .Substring(filePath.IndexOf(viewModelsPathPiece), filePath.LastIndexOf(".") - filePath.IndexOf(viewModelsPathPiece))
                .Replace("\\", ".");

            Type = Assembly.GetExecutingAssembly().GetType(typeFullName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            return new EmailAddressAttribute().IsValid(value)
                ? ValidationResult.Success
                : new ValidationResult(GetErrorMessage(validationContext));
        }

        private string GetErrorMessage(ValidationContext validationContext) {

            var localizer = validationContext.GetService(typeof(IStringLocalizer<>).MakeGenericType(Type)) as IStringLocalizer;

            var errorMessageKey = "EmailErrorMessage";
            var errorMessage = localizer[errorMessageKey];

            return errorMessage == errorMessageKey
                ? "Invalid e-mail."
                : errorMessage;
        }
    }
}
