using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices {

    public class HiddenOpinionsProvider : IHiddenOpinionsProvider {

        IOpinionRepository OpinionRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        public HiddenOpinionsProvider(
            IOpinionRepository opinionRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository) {

            OpinionRepository = opinionRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
        }

        public IEnumerable<HiddenOpinionDetails> Load() {

            foreach (var opinion in OpinionRepository.FindBy(new HiddenOpinionsFilter())) {

                var purchase = PurchaseRepository.FindBy(opinion.PurchaseId);

                var product = ProductRepository.FindBy(purchase.ProductId);

                yield return new HiddenOpinionDetails {
                    Id = opinion.OpinionId.ToString(),
                    ProductName = product.Name,
                    Text = opinion.Text
                };
            }
        }
    }
}
