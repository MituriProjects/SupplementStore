using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class ProductPurchasesFilter : IManyFilter<Purchase> {

        ProductId ProductId { get; }

        public ProductPurchasesFilter(ProductId productId) {

            ProductId = productId;
        }

        public IEnumerable<Purchase> Process(IQueryable<Purchase> entities) {

            return entities.Where(e => e.ProductId == ProductId);
        }
    }
}
