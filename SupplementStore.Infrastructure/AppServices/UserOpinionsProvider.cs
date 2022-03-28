using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.AppModels;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class UserOpinionsProvider : IUserOpinionsProvider {

        IOrderRepository OrderRepository { get; }

        IPurchaseRepository PurchaseRepository { get; }

        IProductRepository ProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public UserOpinionsProvider(
            IOrderRepository orderRepository,
            IPurchaseRepository purchaseRepository,
            IProductRepository productRepository,
            IOpinionRepository opinionRepository) {

            OrderRepository = orderRepository;
            PurchaseRepository = purchaseRepository;
            ProductRepository = productRepository;
            OpinionRepository = opinionRepository;
        }

        public IEnumerable<OpinionDetails> Load(string userId) {

            var userOrders = OrderRepository.FindBy(new UserOrdersFilter(userId));

            var ordersPurchases = PurchaseRepository.FindBy(new OrdersPurchasesFilter(userOrders.Select(e => e.OrderId)));

            var opinions = OpinionRepository.FindBy(ordersPurchases.Select(e => e.OpinionId));

            foreach (var opinion in opinions) {

                var purchase = ordersPurchases
                    .First(e => e.OpinionId == opinion.OpinionId);

                var product = ProductRepository.FindBy(purchase.ProductId);

                var order = userOrders.First(e => e.OrderId == purchase.OrderId);

                yield return OpinionDetailsFactory.Create(opinion, product, order);
            }
        }
    }
}
