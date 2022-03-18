using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionProvider : IOpinionProvider {

        IOrderRepository OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public OpinionProvider(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public OpinionDetails Load(string opinionId) {

            var opinion = OpinionRepository.FindBy(new OpinionId(opinionId));

            var orderProduct = OrderProductRepository.FindBy(opinion.OrderProductId);

            var order = OrderRepository.FindBy(orderProduct.OrderId);

            var product = ProductRepository.FindBy(orderProduct.ProductId);

            return new OpinionDetails {
                Id = opinion.OpinionId.ToString(),
                ProductName = product.Name,
                BuyingDate = order.CreatedOn,
                Stars = opinion.Grade.Stars,
                Text = opinion.Text
            };
        }
    }
}
