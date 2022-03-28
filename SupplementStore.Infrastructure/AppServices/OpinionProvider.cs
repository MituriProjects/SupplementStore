using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionProvider : IOpinionProvider {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public OpinionProvider(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public OpinionDetails Load(string opinionId) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            var purchase = PurchaseRepository.FindBy(opinion.PurchaseId);

            var order = OrderRepository.FindBy(purchase.OrderId);

            var product = ProductRepository.FindBy(purchase.ProductId);

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
