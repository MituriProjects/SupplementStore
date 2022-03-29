using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.AppModels;

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

            return ProductDetailsFactory.Create(product);
        }
    }
}
