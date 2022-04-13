using SupplementStore.Application.Models;
using SupplementStore.Domain.Opinions;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public OpinionDetails Load(string opinionId) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            var purchase = PurchaseRepository.FindBy(opinion.PurchaseId);

            var order = OrderRepository.FindBy(purchase.OrderId);

            var product = ProductRepository.FindBy(purchase.ProductId);

            return opinion.ToDetails(product, order);
        }
    }
}
