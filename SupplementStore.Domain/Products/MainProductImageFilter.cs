using System.Linq;

namespace SupplementStore.Domain.Products {

    public class MainProductImageFilter : IFilter<ProductImage> {

        ProductId ProductId { get; }

        public MainProductImageFilter(ProductId productId) {

            ProductId = productId;
        }

        public ProductImage Process(IQueryable<ProductImage> entities) {

            return entities.FirstOrDefault(e => e.ProductId == ProductId && e.IsMain);
        }
    }
}
