using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionProductProvider : IOpinionProductProvider {

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        public OpinionProductProvider(
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository) {

            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
        }

        public ProductDetails Load(string opinionId) {

            var purchase = PurchaseRepository.FindBy(new OpinionPurchaseFilter(new OpinionId(opinionId)));

            var product = ProductRepository.FindBy(purchase.ProductId);

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
