using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

        public void AppointMain(string productId, string name) {

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
