using SupplementStore.Application.Results;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductToOpineProvider : IProductToOpineProvider {

        IOrderRepository OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public ProductToOpineProvider(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public ProductToOpineResult Load(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var orderProducts = OrderProductRepository.FindBy(new OrdersProductsFilter(userOrders.Select(e => e.OrderId)));

            var orderProduct = orderProducts
                .FirstOrDefault(e => e.OpinionId == null);

            if (orderProduct == null)
                return ProductToOpineResult.Empty;

            var product = ProductRepository.FindBy(orderProduct.ProductId);

            var order = userOrders
                .First(e => e.OrderId == orderProduct.OrderId);

            return new ProductToOpineResult {
                OrderProductId = orderProduct.OrderProductId.ToString(),
                ProductName = product.Name,
                BuyingDate = order.CreatedOn
            };
        }
    }
}
