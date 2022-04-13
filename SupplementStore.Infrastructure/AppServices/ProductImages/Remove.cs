using SupplementStore.Application.Results;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

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
