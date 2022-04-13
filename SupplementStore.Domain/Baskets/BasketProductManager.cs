using SupplementStore.Domain.Products;

namespace SupplementStore.Domain.Baskets {

    public class BasketProductManager {

        IProductRepository ProductRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        public BasketProductManager(
            IProductRepository productRepository,
            IBasketProductRepository basketProductRepository) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
        }

        public void Adjust(string userId, string productId, int quantity) {

            if (string.IsNullOrEmpty(userId))
                return;

            var productIdObject = new ProductId(productId);

            var product = ProductRepository.FindBy(productIdObject);

            if (product == null)
                return;

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, productIdObject));

            if (basketProduct == null) {

                try {

                    basketProduct = new BasketProduct {
                        UserId = userId,
                        ProductId = productIdObject,
                        Quantity = quantity
                    };
                }
                catch {

                    return;
                }

                BasketProductRepository.Add(basketProduct);
            }
            else {

                basketProduct.Quantity += quantity;
            }
        }
    }
}
