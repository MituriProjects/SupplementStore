using SupplementStore.Application.Models;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppModels {

    static class PurchaseDetailsFactory {

        public static IEnumerable<PurchaseDetails> Create(IEnumerable<Purchase> purchases, IEnumerable<Product> products) {

            return purchases.Select(e => new PurchaseDetails {
                ProductId = e.ProductId.ToString(),
                ProductName = products.First(p => p.ProductId == e.ProductId).Name,
                ProductPrice = products.First(p => p.ProductId == e.ProductId).Price,
                Quantity = e.Quantity
            }).ToList();
        }
    }
}
