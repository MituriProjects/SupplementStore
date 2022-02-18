﻿using System;

namespace SupplementStore.Domain.Orders {

    public class OrderProductId : IdBase {

        public OrderProductId(Guid id) : base(id) {
        }

        public OrderProductId(string id) : base(id) {
        }
    }
}