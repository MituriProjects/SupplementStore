using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Product {

    public class ProductEditViewModel {

        public string Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cena jest wymagana")]
        [Display(Name = "Cena")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być powyżej zera")]
        public decimal? Price { get; set; } = 0;
    }
}
