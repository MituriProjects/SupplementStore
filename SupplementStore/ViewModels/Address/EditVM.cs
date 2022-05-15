using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Address {

    public class EditVM {

        public string Id { get; set; }

        [Required(ErrorMessage = "StreetRequiredErrorMessage")]
        [Label]
        public string Street { get; set; }

        [Required(ErrorMessage = "PostalCodeRequiredErrorMessage")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "RegularExpressionErrorMessage")]
        [Label]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "CityRequiredErrorMessage")]
        [Label]
        public string City { get; set; }
    }
}
