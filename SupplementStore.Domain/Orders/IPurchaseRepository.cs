namespace SupplementStore.Domain.Orders {

    public interface IPurchaseRepository : IRepository<Purchase> {
        Purchase FindBy(PurchaseId purchaseId);
    }
}
