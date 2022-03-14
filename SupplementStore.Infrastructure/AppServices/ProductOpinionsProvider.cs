using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices {

    public class ProductOpinionsProvider : IProductOpinionsProvider {

        IOrderProductRepository OrderProductRepository { get; }

        IOpinionRepository OpinionRepository { get; }

        public ProductOpinionsProvider(
            IOrderProductRepository orderProductRepository,
            IOpinionRepository opinionRepository) {

            OrderProductRepository = orderProductRepository;
            OpinionRepository = opinionRepository;
        }

        public IEnumerable<ProductOpinionDetails> Load(string productId) {

            var orderProducts = OrderProductRepository.FindBy(new ProductOrderProductsFilter(new ProductId(productId)));

            var opinions = OpinionRepository.FindBy(orderProducts.Select(e => e.OpinionId));

            foreach (var opinion in opinions) {

                yield return new ProductOpinionDetails {
                    Stars = opinion.Grade.Stars,
                    Text = opinion.Text
                };
            }
        }
    }
}
