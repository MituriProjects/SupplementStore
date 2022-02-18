﻿using SupplementStore.Domain.Products;
using SupplementStore.Domain.Shared;
using System;

namespace SupplementStore.Domain.Orders {

    public class OrderProduct : Entity {

        public OrderProductId OrderProductId { get; private set; } = new OrderProductId(Guid.Empty);

        Quantity ProductQuantity { get; set; }

        Guid Order_Id {
            get => OrderId.Id;
            set => OrderId = new OrderId(value);
        }

        public OrderId OrderId { get; set; }

        Guid Product_Id {
            get => ProductId.Id;
            set => ProductId = new ProductId(value);
        }

        public ProductId ProductId { get; set; }

        public int Quantity {
            get => ProductQuantity.Value;
            set => ProductQuantity = new Quantity(value);
        }
    }
}