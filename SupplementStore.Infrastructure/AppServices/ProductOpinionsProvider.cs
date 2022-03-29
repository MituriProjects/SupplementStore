using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductOpinionsProvider : IProductOpinionsProvider {

        IPurchaseRepository PurchaseRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public ProductOpinionsProvider(
            IPurchaseRepository purchaseRepository,
            IOpinionRepository opinionRepository) {

            PurchaseRepository = purchaseRepository;
            OpinionRepository = opinionRepository;
        }

        public IEnumerable<ProductOpinionDetails> Load(string productId) {

            var purchases = PurchaseRepository.FindBy(new ProductPurchasesFilter(new ProductId(productId)));

            var opinions = OpinionRepository.FindBy(purchases.Select(e => e.OpinionId));

            foreach (var opinion in opinions) {

                yield return new ProductOpinionDetails {
                    Id = opinion.OpinionId.ToString(),
                    Stars = opinion.Rating.Stars,
                    Text = opinion.Text,
                    IsHidden = opinion.IsHidden
                };
            }
        }
    }
}
