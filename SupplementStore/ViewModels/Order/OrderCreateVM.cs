using SupplementStore.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Order {

    public class OrderCreateVM {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [Required(ErrorMessage = "AddressRequiredErrorMessage")]
        [Label]
        public string Address { get; set; }

        [Required(ErrorMessage = "PostalCodeRequiredErrorMessage")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "PostalCodeRegularExpressionErrorMessage")]
        [Label]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "CityRequiredErrorMessage")]
        [Label]
        public string City { get; set; }

        [Label]
        public bool IsAddressToBeSaved { get; set; }
    }
}
