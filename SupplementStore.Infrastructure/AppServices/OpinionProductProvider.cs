using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices {

    public class OpinionProductProvider : IOpinionProductProvider {

        IOrderProductRepository OrderProductRepository { get; }

        IProductRepository ProductRepository { get; }

        public OpinionProductProvider(
            IOrderProductRepository orderProductRepository,
            IProductRepository productRepository) {

            OrderProductRepository = orderProductRepository;
            ProductRepository = productRepository;
        }

        public ProductDetails Load(string opinionId) {

            var orderProduct = OrderProductRepository.FindBy(new OpinionOrderProductFilter(new OpinionId(opinionId)));

            var product = ProductRepository.FindBy(orderProduct.ProductId);

            return new ProductDetails {
                Id = product.ProductId.ToString(),
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
