using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Collections.Generic;

namespace SupplementStore.Infrastructure.AppServices {

    public class HiddenOpinionsProvider : IHiddenOpinionsProvider {

        IOpinionRepository OpinionRepository { get; }

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        public HiddenOpinionsProvider(
            IOpinionRepository opinionRepository,
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository) {

            OpinionRepository = opinionRepository;
            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
        }

        public IEnumerable<HiddenOpinionDetails> Load() {

            foreach (var opinion in OpinionRepository.FindBy(new HiddenOpinionsFilter())) {

                var orderProduct = OrderProductRepository.FindBy(opinion.OrderProductId);

                var product = ProductRepository.FindBy(orderProduct.ProductId);

                yield return new HiddenOpinionDetails {
                    Id = opinion.OpinionId.ToString(),
                    ProductName = product.Name,
                    Text = opinion.Text
                };
            }
        }
    }
}
