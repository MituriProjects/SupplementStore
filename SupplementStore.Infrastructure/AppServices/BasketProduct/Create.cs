namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService {

        public void Create(string userId, string productId, int quantity) {

            BasketProductManager.Adjust(userId, productId, quantity);

            DomainApprover.SaveChanges();
        }
    }
}
