using SupplementStore.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Order {

    public class OrderCreateViewModel {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }
    }
}
