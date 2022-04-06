using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository {

        public ProductImageRepository(IDocument<ProductImage> document) : base(document) {
        }

        public ProductImage FindBy(ProductImageId productImageId) {

            return Document.All
                .FirstOrDefault(e => e.ProductImageId == productImageId);
        }
    }
}
