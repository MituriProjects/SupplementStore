using System;

namespace SupplementStore.Domain.Orders {

    public class Order : Entity {

        public string UserId { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
