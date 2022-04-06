using System.Linq;

namespace SupplementStore.Domain.Products {

    public class ProductImageFilter : IFilter<ProductImage> {

        ProductId ProductId { get; }

        string Name { get; }

        public ProductImageFilter(ProductId productId, string name) {

            ProductId = productId;
            Name = name;
        }

        public ProductImage Process(IQueryable<ProductImage> entities) {

            return entities.FirstOrDefault(e => e.ProductId == ProductId && e.Name == Name);
        }
    }
}
