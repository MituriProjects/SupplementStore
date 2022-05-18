using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Order {

    public class CreateVM {

        public IEnumerable<BasketProductDetails> BasketProducts { get; set; }

        [IsRequired]
        [Label]
        public string Street { get; set; }

        [IsRequired]
        [PostalCode]
        [Label]
        public string PostalCode { get; set; }

        [IsRequired]
        [Label]
        public string City { get; set; }

        [Label]
        public bool IsAddressToBeSaved { get; set; }
    }
}
