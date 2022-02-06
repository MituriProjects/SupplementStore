using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductsProvider : IBasketProductsProvider {

        IDocument<Product> ProductDocument { get; }

        IDocument<BasketProduct> BasketProductDocument { get; }

        public BasketProductsProvider(
            IDocument<Product> productDocument,
            IDocument<BasketProduct> basketProductDocument) {

            ProductDocument = productDocument;
            BasketProductDocument = basketProductDocument;
        }

        public IEnumerable<BasketProductDetails> Load(string userId) {

            foreach (var basketProduct in BasketProductDocument.All.Where(e => e.UserId == userId).ToList()) {

                var product = ProductDocument.All.First(e => e.Id == basketProduct.ProductId);

                yield return new BasketProductDetails {
                    Id = basketProduct.Id.ToString(),
                    ProductId = basketProduct.ProductId.ToString(),
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    Quantity = basketProduct.Quantity
                };
            }
        }
    }
}
