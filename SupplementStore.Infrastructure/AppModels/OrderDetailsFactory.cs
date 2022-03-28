using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppModels {

    static class OrderDetailsFactory {

        public static OrderDetails Create(Order order, IEnumerable<PurchaseDetails> purchaseDetailsCollection) {

            return new OrderDetails {
                Id = order.OrderId.ToString(),
                UserId = order.UserId,
                Address = order.Address.Street,
                PostalCode = order.Address.PostalCode,
                City = order.Address.City,
                CreatedOn = order.CreatedOn,
                Purchases = purchaseDetailsCollection
            };
        }
    }
}
