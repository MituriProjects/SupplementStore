using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductRemover : IBasketProductRemover {

        IBasketProductRepository BasketProductRepository { get; }

        IDomainApprover DomainApprover { get; }

        public BasketProductRemover(
            IBasketProductRepository basketProductRepository,
            IDomainApprover domainApprover) {

            BasketProductRepository = basketProductRepository;
            DomainApprover = domainApprover;
        }

        public void Remove(string userId, string productId) {

            var basketProduct = BasketProductRepository.FindBy(new UserBasketProductFilter(userId, new ProductId(productId)));

            if (basketProduct == null)
                return;

            BasketProductRepository.Delete(basketProduct.BasketProductId);

            DomainApprover.SaveChanges();
        }
    }
}
