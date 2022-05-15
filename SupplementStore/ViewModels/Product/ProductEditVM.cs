using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Product {

    public class ProductEditVM {

        public string Id { get; set; }

        [IsRequired(typeof(ProductEditVM))]
        [Label]
        public string Name { get; set; }

        [IsRequired(typeof(ProductEditVM))]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceAboveZeroErrorMessage")]
        [Label]
        public decimal? Price { get; set; } = 0;
    }
}
