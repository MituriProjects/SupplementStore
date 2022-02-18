using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductProvider : IBasketProductProvider {

        IProductRepository ProductRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        public BasketProductProvider(
            IProductRepository productRepository,
            IBasketProductRepository basketProductRepository) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
        }

        public BasketProductDetails Load(string id) {

            var basketProduct = BasketProductRepository.FindBy(new BasketProductId(id));

            if (basketProduct == null)
                return null;

            var product = ProductRepository.FindBy(basketProduct.ProductId);

            if (product == null)
                return null;

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
