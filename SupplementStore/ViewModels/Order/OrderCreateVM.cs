using SupplementStore.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Order {

    public class OrderCreateVM {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [IsRequired]
        [Label]
        public string Street { get; set; }

        [IsRequired]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "PostalCodeRegularExpressionErrorMessage")]
        [Label]
        public string PostalCode { get; set; }

        [IsRequired]
        [Label]
        public string City { get; set; }

        [Label]
        public bool IsAddressToBeSaved { get; set; }
    }
}
