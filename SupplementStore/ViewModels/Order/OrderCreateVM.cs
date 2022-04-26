﻿using SupplementStore.Application.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Order {

    public class OrderCreateVM {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$")]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "Zapisz adres")]
        public bool IsAddressToBeSaved { get; set; }
    }
}