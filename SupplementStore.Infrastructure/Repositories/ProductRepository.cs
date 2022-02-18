using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class ProductRepository : Repository<Product>, IProductRepository {

        public ProductRepository(IDocument<Product> document) : base(document) {
        }

        public Product FindBy(ProductId productId) {

            return Document.All
                .FirstOrDefault(e => e.ProductId == productId);
        }
    }
}
