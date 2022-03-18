using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionsProvider : IOpinionsProvider {

        IOrderRepository OrderRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public OpinionsProvider(
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public IEnumerable<OpinionDetails> Load(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var orderProducts = OrderProductRepository.FindBy(new OrdersProductsFilter(userOrders.Select(e => e.OrderId)));

            var opinions = OpinionRepository.FindBy(orderProducts.Select(e => e.OpinionId));

            foreach (var opinion in opinions) {

                var orderProduct = orderProducts
                    .First(e => e.OpinionId == opinion.OpinionId);

                yield return new OpinionDetails {
                    Id = opinion.OpinionId.ToString(),
                    ProductName = ProductRepository.FindBy(orderProduct.ProductId).Name,
                    BuyingDate = userOrders.First(e => e.OrderId == orderProduct.OrderId).CreatedOn,
                    Stars = opinion.Grade.Stars,
                    Text = opinion.Text
                };
            }
        }
    }
}
