using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Address {

    public class CreateVM {

        [Required(ErrorMessage = "Nazwa ulicy jest wymagana.")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Kod pocztowy ma niewłaściwy format.")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }
    }
}
