using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class MainProductImageAppointer : IMainProductImageAppointer {

        IProductImageRepository ProductImageRepository { get; }

        IDomainApprover DomainApprover { get; }

        public MainProductImageAppointer(
            IProductImageRepository productImageRepository,
            IDomainApprover domainApprover) {

            ProductImageRepository = productImageRepository;
            DomainApprover = domainApprover;
        }

        public void Perform(string productId, string name) {

            var productImage = ProductImageRepository.FindBy(new ProductImageFilter(new ProductId(productId), name));

            if (productImage == null)
                return;

            var mainProductImage = ProductImageRepository.FindBy(new MainProductImageFilter(new ProductId(productId)));

            if (mainProductImage != null)
                mainProductImage.IsMain = false;

            productImage.IsMain = true;

            DomainApprover.SaveChanges();
        }
    }
}
