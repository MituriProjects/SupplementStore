using SupplementStore.Application.Models;
using SupplementStore.Domain.Opinions;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public IEnumerable<HiddenOpinionDetails> LoadHidden() {

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
