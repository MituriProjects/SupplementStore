using SupplementStore.Domain.Baskets;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class BasketProductRepository : Repository<BasketProduct>, IBasketProductRepository {

        public BasketProductRepository(IDocument<BasketProduct> document) : base(document) {
        }

        public BasketProduct FindBy(BasketProductId basketProductId) {

            return Document.All
                .FirstOrDefault(e => e.BasketProductId == basketProductId);
        }
    }
}
