using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SupplementStore.ViewModels {

    public class IsRequiredAttribute : RequiredAttribute {

        Type Type { get; }

        public IsRequiredAttribute([CallerFilePath]string filePath = null, [CallerMemberName]string callerName = null) {

            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetName()
                .Name;

            var viewModelsPathPiece = assemblyName + "\\ViewModels";

            var typeFullName = filePath
                .Substring(filePath.IndexOf(viewModelsPathPiece), filePath.LastIndexOf(".") - filePath.IndexOf(viewModelsPathPiece))
                .Replace("\\", ".");

            Type = Assembly.GetExecutingAssembly().GetType(typeFullName);

            ErrorMessage = $"{callerName}RequiredErrorMessage";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var result = base.IsValid(value, validationContext);

            if (result != null)
                result.ErrorMessage = GetErrorMessage(validationContext);

            return result;
        }

        private string GetErrorMessage(ValidationContext validationContext) {

            var localizer = validationContext.GetService(typeof(IStringLocalizer<>).MakeGenericType(Type)) as IStringLocalizer;

            return localizer[ErrorMessage];
        }
    }
}
