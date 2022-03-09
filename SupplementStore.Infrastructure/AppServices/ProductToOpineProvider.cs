using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductToOpineProvider : IProductToOpineProvider {

        IOrderRepository OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public ProductToOpineProvider(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            OpinionRepository = opinionRepository;
        }

        public ProductToOpineResult Load(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var orderProducts = OrderProductRepository.FindBy(new OrdersProductsFilter(userOrders.Select(e => e.OrderId)));

            var orderProduct = orderProducts
                .Where(e => e.OpinionId == null)
                .FirstOrDefault();

            return new ProductToOpineResult {
                OrderProductId = orderProduct?.OrderProductId.ToString()
            };
        }
    }
}
