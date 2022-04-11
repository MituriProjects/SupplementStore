using SupplementStore.Application.Models;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Infrastructure.AppModels;

namespace SupplementStore.Infrastructure.AppServices.Opinions {

    public partial class OpinionService {

        public ProductDetails LoadOpinionProduct(string opinionId) {

            var purchase = PurchaseRepository.FindBy(new OpinionPurchaseFilter(new OpinionId(opinionId)));

            var product = ProductRepository.FindBy(purchase.ProductId);

            return ProductDetailsFactory.Create(product);
        }
    }
}
