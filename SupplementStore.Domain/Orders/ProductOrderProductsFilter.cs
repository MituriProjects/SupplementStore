using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class ProductOrderProductsFilter : IManyFilter<OrderProduct> {

        ProductId ProductId { get; }

        public ProductOrderProductsFilter(ProductId productId) {

            ProductId = productId;
        }

        public IEnumerable<OrderProduct> Process(IQueryable<OrderProduct> entities) {

            return entities.Where(e => e.ProductId == ProductId);
        }
    }
}
