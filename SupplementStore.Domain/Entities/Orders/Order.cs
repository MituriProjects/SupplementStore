using System;

namespace SupplementStore.Domain.Entities.Orders {

    public class Order : Entity {

        public string UserId { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        protected override void Validate() {
        }
    }
}
