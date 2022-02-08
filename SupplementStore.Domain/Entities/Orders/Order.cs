using System;
using System.Text.RegularExpressions;

namespace SupplementStore.Domain.Entities.Orders {

    public class Order : Entity {

        public string UserId { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        protected override void Validate() {

            if (Regex.IsMatch(PostalCode, @"^\d{2}-\d{3}$") == false)
                AddBrokenRule(OrderBusinessRules.PostalCodeInvalidFormat);
        }
    }
}
