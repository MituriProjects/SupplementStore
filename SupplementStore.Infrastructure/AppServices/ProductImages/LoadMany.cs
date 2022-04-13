using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.ModelMapping;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

        public IEnumerable<ProductImageDetails> LoadMany(string productId) {

            return ProductImageRepository
                .FindBy(new ProductImagesFilter(new ProductId(productId)))
                .ToDetails();
        }
    }
}
