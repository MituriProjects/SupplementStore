using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService {

        public ProductDetails Load(string productId) {

            var product = ProductRepository.FindBy(new ProductId(productId));

            if (product == null)
                return null;

            return product.ToDetails();
        }
    }
}
