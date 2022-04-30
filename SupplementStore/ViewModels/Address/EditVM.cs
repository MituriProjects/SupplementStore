using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Address {

    public class EditVM {

        public string Id { get; set; }

        [Required(ErrorMessage = "StreetRequiredErrorMessage")]
        [Display(Name = "StreetLabel")]
        public string Street { get; set; }

        [Required(ErrorMessage = "PostalCodeRequiredErrorMessage")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "RegularExpressionErrorMessage")]
        [Display(Name = "PostalCodeLabel")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "CityRequiredErrorMessage")]
        [Display(Name = "CityLabel")]
        public string City { get; set; }
    }
}
