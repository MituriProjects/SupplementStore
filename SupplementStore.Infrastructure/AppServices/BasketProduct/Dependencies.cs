using SupplementStore.Application.Services;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.BasketProduct {

    public partial class BasketProductService : IBasketProductService {

        IProductRepository ProductRepository { get; }

        IBasketProductRepository BasketProductRepository { get; }

        BasketProductManager BasketProductManager { get; }

        IDomainApprover DomainApprover { get; }

        public BasketProductService(
            IProductRepository productRepository,
            IBasketProductRepository basketProductRepository,
            BasketProductManager basketProductManager,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            BasketProductRepository = basketProductRepository;
            BasketProductManager = basketProductManager;
            DomainApprover = domainApprover;
        }
    }
}
