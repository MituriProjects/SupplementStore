using SupplementStore.Application.Models;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class OrderExtensions {

        public static OrderDetails ToDetails(this Order order, Address address = null, IEnumerable<PurchaseDetails> purchaseDetailsCollection = null) {

            return new OrderDetails {
                Id = order.OrderId.ToString(),
                UserId = order.UserId,
                Address = address?.Street,
                PostalCode = address?.PostalCode.Value,
                City = address?.City,
                CreatedOn = order.CreatedOn,
                Purchases = purchaseDetailsCollection ?? new List<PurchaseDetails>()
            };
        }
    }
}
