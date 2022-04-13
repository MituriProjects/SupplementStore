using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService {

        public void Remove(string userId, string productId) {

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, new ProductId(productId)));

            if (basketProduct == null)
                return;

            BasketProductRepository.Delete(basketProduct.BasketProductId);

            DomainApprover.SaveChanges();
        }
    }
}
