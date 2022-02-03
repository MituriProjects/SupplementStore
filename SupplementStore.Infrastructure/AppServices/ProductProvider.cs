using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductProvider : IProductProvider {

        IDocument<Product> ProductDocument { get; }

        public ProductProvider(IDocument<Product> productDocument) {

            ProductDocument = productDocument;
        }

        public ProductDetails Load(string productId) {

            if (Guid.TryParse(productId, out var guidProductId) == false)
                return null;

            var product = ProductDocument.All.FirstOrDefault(e => e.Id.Equals(guidProductId));

            if (product == null)
                return null;

            return new ProductDetails {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
