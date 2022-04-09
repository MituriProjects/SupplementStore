using SupplementStore.Application.Models;
using SupplementStore.Domain.Baskets;

namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService {

        public void Update(BasketProductDetails basketProductDetails) {

            var basketProduct = BasketProductRepository.FindBy(new BasketProductId(basketProductDetails.Id));

            if (basketProduct == null)
                return;

            basketProduct.Quantity = basketProductDetails.Quantity;

            DomainApprover.SaveChanges();
        }
    }
}
