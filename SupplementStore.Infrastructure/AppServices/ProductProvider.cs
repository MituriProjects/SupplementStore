using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductProvider : IProductProvider {

        IDocument<Product> ProductDocument { get; }

        public ProductProvider(IDocument<Product> productDocument) {

            ProductDocument = productDocument;
        }

        public ProductProviderResult Load(ProductProviderArgs args) {

            var products = ProductDocument.All
                .Skip(args.Skip)
                .Take(args.Take)
                .Select(e => new ProductDetails {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    Price = e.Price
                });

            return new ProductProviderResult {
                AllProductsCount = ProductDocument.All.Count(),
                Products = products
            };
        }
    }
}
