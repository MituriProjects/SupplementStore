using SupplementStore.Application.Results;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

        public ProductImageCreatorResult Create(string productId, string imageName) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return ProductImageCreatorResult.Failed;

            var productImage = ProductImageRepository.FindBy(new ProductImageFilter(product.ProductId, imageName));

            if (productImage != null)
                return ProductImageCreatorResult.Failed;

            ProductImageRepository.Add(new ProductImage {
                ProductId = new ProductId(productId),
                Name = imageName
            });

            DomainApprover.SaveChanges();

            return ProductImageCreatorResult.Succeeded;
        }
    }
}
