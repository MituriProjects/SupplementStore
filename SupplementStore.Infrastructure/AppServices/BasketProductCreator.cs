using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductCreator : IBasketProductCreator {

        IProductRepository ProductRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public BasketProductCreator(
            IProductRepository productRepository,
            IBasketProductRepository basketProductRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
            DomainApprover = domainApprover;
        }

        public void Create(string userId, string productId, int quantity) {

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

            DomainApprover.SaveChanges();
        }
    }
}
