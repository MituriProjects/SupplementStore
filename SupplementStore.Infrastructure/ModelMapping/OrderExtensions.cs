using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class OrderExtensions {

        public static OrderDetails ToDetails(this Order order, IEnumerable<PurchaseDetails> purchaseDetailsCollection = null) {

            return new OrderDetails {
                Id = order.OrderId.ToString(),
                UserId = order.UserId,
                Address = order.Address.Street,
                PostalCode = order.Address.PostalCode,
                City = order.Address.City,
                CreatedOn = order.CreatedOn,
                Purchases = purchaseDetailsCollection ?? new List<PurchaseDetails>()
            };
        }
    }
}
