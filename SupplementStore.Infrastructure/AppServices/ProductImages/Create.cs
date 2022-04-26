using SupplementStore.Application.Results;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

        public ProductImageCreateResult Create(string productId, string imageName) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return ProductImageCreateResult.Failed;

            var productImage = ProductImageRepository.FindBy(new ProductImageFilter(product.ProductId, imageName));

            if (productImage != null)
                return ProductImageCreateResult.Failed;

            ProductImageRepository.Add(new ProductImage {
                ProductId = new ProductId(productId),
                Name = imageName
            });

            DomainApprover.SaveChanges();

            return ProductImageCreateResult.Succeeded;
        }
    }
}
