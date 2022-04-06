using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Products {

    public class ProductImagesFilter : IManyFilter<ProductImage> {

        ProductId ProductId { get; }

        public ProductImagesFilter(ProductId productId) {

            ProductId = productId;
        }

        public IEnumerable<ProductImage> Process(IQueryable<ProductImage> entities) {

            return entities.Where(e => e.ProductId == ProductId);
        }
    }
}
