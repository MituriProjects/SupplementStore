using SupplementStore.Application.Models;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class BasketProductExtensions {

        public static BasketProductDetails ToDetails(this BasketProduct basketProduct, Product product) {

            return new BasketProductDetails {
                Id = basketProduct.BasketProductId.ToString(),
                ProductId = basketProduct.ProductId.ToString(),
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = basketProduct.Quantity
            };
        }
    }
}
