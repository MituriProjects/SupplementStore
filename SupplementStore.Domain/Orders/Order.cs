using SupplementStore.Domain.Addresses;
using System;

namespace SupplementStore.Domain.Orders {

    public class Order : UserEntity {

        public OrderId OrderId { get; private set; } = new OrderId(Guid.Empty);

        Guid Address_Id {
            get => AddressId.Id;
            set => AddressId = new AddressId(value);
        }

        public AddressId AddressId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
