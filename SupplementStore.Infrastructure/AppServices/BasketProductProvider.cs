using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class BasketProductProvider : IBasketProductProvider {

        IDocument<Product> ProductDocument { get; }

        IDocument<BasketProduct> BasketProductDocument { get; }

        public BasketProductProvider(
            IDocument<Product> productDocument,
            IDocument<BasketProduct> basketProductDocument) {

            ProductDocument = productDocument;
            BasketProductDocument = basketProductDocument;
        }

        public BasketProductDetails Load(string id) {

            var basketProduct = BasketProductDocument.All
                .FirstOrDefault(e => e.Id == Guid.Parse(id));

            if (basketProduct == null)
                return null;

            var product = ProductDocument.All
                .FirstOrDefault(e => e.Id == basketProduct.ProductId);

            if (product == null)
                return null;

            return new BasketProductDetails {
                Id = basketProduct.Id.ToString(),
                ProductId = basketProduct.ProductId.ToString(),
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = basketProduct.Quantity
            };
        }
    }
}
