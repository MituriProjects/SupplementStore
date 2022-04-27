using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Product {

    public class ProductEditVM {

        public string Id { get; set; }

        [Required(ErrorMessage = "NameRequiredErrorMessage")]
        [Display(Name = "NameLabel")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PriceRequiredErrorMessage")]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceAboveZeroErrorMessage")]
        [Display(Name = "PriceLabel")]
        public decimal? Price { get; set; } = 0;
    }
}
