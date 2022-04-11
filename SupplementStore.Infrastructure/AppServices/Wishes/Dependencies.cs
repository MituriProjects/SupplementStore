using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.AppServices.Wishes {

    public partial class WishService : IWishService {

        IProductRepository ProductRepository { get; }

        IWishRepository WishRepository { get; }

        IDomainApprover DomainApprover { get; }

        public WishService(
            IProductRepository productRepository,
            IWishRepository wishRepository,
            IDomainApprover domainApprover) {

            ProductRepository = productRepository;
            WishRepository = wishRepository;
            DomainApprover = domainApprover;
        }
    }
}
