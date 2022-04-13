using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class ProductImageExtensions {

        public static IEnumerable<ProductImageDetails> ToDetails(this IEnumerable<ProductImage> productImages) {

            return productImages.Select(e => new ProductImageDetails {
                Name = e.Name,
                IsMain = e.IsMain
            }).ToList();
        }
    }
}
