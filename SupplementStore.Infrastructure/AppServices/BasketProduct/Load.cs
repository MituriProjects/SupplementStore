using SupplementStore.Application.Models;
using SupplementStore.Domain.Baskets;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService {

        public BasketProductDetails Load(string id) {

            var basketProduct = BasketProductRepository.FindBy(new BasketProductId(id));

            if (basketProduct == null)
                return null;

            var product = ProductRepository.FindBy(basketProduct.ProductId);

            return basketProduct.ToDetails(product);
        }
    }
}
