using System;

namespace SupplementStore.Domain.Orders {

    public class Order : Entity {

        public OrderId OrderId { get; private set; } = new OrderId(Guid.Empty);

        public string UserId { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
