using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Repositories {

    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository {

        public ProductImageRepository(IDocument<ProductImage> document) : base(document) {
        }
    }
}
