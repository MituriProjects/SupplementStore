using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Address {

    public class CreateVM {

        [IsRequired]
        [Label]
        public string Street { get; set; }

        [IsRequired]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "RegularExpressionErrorMessage")]
        [Label]
        public string PostalCode { get; set; }

        [IsRequired]
        [Label]
        public string City { get; set; }
    }
}
