using SupplementStore.Application.Models;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class OpinionExtensions {

        public static OpinionDetails ToDetails(this Opinion opinion, Product product, Order order) {

            return new OpinionDetails {
                Id = opinion.OpinionId.ToString(),
                ProductName = product.Name,
                BuyingDate = order.CreatedOn,
                Stars = opinion.Rating.Stars,
                Text = opinion.Text
            };
        }
    }
}
