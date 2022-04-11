using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.ProductImages {

    public partial class ProductImageService {

        public IEnumerable<ProductImageDetails> LoadMany(string productId) {

            return ProductImageRepository
                .FindBy(new ProductImagesFilter(new ProductId(productId)))
                .Select(e => new ProductImageDetails {
                    Name = e.Name,
                    IsMain = e.IsMain
                })
                .ToList();
        }
    }
}
