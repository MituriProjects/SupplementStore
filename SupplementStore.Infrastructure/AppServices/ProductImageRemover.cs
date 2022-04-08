using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductImageRemover : IProductImageRemover {

        IProductImageRepository ProductImageRepository { get; }

        IDomainApprover DomainApprover { get; }

        public ProductImageRemover(
            IProductImageRepository productImageRepository,
            IDomainApprover domainApprover) {

            ProductImageRepository = productImageRepository;
            DomainApprover = domainApprover;
        }

        public ProductImageRemoverResult Remove(string productId, string name) {

            var productImage = ProductImageRepository.FindBy(new ProductImageFilter(new ProductId(productId), name));

            if (productImage == null)
                return ProductImageRemoverResult.Failed;

            ProductImageRepository.Delete(productImage.ProductImageId);

            DomainApprover.SaveChanges();

            return ProductImageRemoverResult.Succeeded;
        }
    }
}
