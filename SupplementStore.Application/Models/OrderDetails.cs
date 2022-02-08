using System;
using System.Collections.Generic;

namespace SupplementStore.Application.Models {

    public class OrderDetails {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<OrderProductDetails> OrderProducts { get; set; }
    }
}
