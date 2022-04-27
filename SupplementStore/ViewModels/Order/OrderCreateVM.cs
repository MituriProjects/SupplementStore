using SupplementStore.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Order {

    public class OrderCreateVM {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [Required(ErrorMessage = "AddressRequiredErrorMessage")]
        [Display(Name = "AddressLabel")]
        public string Address { get; set; }

        [Required(ErrorMessage = "PostalCodeRequiredErrorMessage")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "PostalCodeRegularExpressionErrorMessage")]
        [Display(Name = "PostalCodeLabel")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "CityRequiredErrorMessage")]
        [Display(Name = "CityLabel")]
        public string City { get; set; }

        [Display(Name = "SaveAddressLabel")]
        public bool IsAddressToBeSaved { get; set; }
    }
}
