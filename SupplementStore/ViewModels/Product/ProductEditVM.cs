using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Product {

    public class ProductEditVM {

        public string Id { get; set; }

        [Required(ErrorMessage = "NameRequiredErrorMessage")]
        [Label]
        public string Name { get; set; }

        [Required(ErrorMessage = "PriceRequiredErrorMessage")]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceAboveZeroErrorMessage")]
        [Label]
        public decimal? Price { get; set; } = 0;
    }
}
