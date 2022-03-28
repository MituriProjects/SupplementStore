using SupplementStore.Domain.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository {

        public PurchaseRepository(IDocument<Purchase> document) : base(document) {
        }

        public Purchase FindBy(PurchaseId purchaseId) {

            return Document.All
                .FirstOrDefault(e => e.PurchaseId == purchaseId);
        }
    }
}
