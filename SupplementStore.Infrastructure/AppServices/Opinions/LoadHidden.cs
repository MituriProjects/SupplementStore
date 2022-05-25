using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public HiddenOpinionListResult LoadHidden(HiddenOpinionListArgs args) {

            var hiddenOpinions = OpinionRepository
                .FindBy(new PagingFilter<Opinion>(args.Skip, args.Take, new HiddenOpinionsFilter()));

            var loadedHiddenOpinions = new List<HiddenOpinionDetails>();
            foreach (var opinion in hiddenOpinions) {

                var purchase = PurchaseRepository.FindBy(opinion.PurchaseId);

                var product = ProductRepository.FindBy(purchase.ProductId);

                loadedHiddenOpinions.Add(new HiddenOpinionDetails {
                    Id = opinion.OpinionId.ToString(),
                    ProductName = product.Name,
                    Text = opinion.Text
                });
            }

            return new HiddenOpinionListResult {
                AllHiddenOpinionsCount = hiddenOpinions.Count(),
                HiddenOpinions = loadedHiddenOpinions
            };
        }
    }
}
