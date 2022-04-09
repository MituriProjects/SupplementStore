using SupplementStore.Application.Models;
using SupplementStore.Domain.Baskets;
using SupplementStore.Infrastructure.AppModels;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService {

        public IEnumerable<BasketProductDetails> LoadMany(string userId) {

            foreach (var basketProduct in BasketProductRepository.FindBy(new UserBasketProductsFilter(userId))) {

                var product = ProductRepository.FindBy(basketProduct.ProductId);

                yield return BasketProductDetailsFactory.Create(basketProduct, product);
            }
        }
    }
}
