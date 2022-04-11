using SupplementStore.Application.Models;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.AppServices.Products {

    public partial class ProductService {

        public void Update(ProductDetails productDetails) {

            var product = ProductRepository.FindBy(new ProductId(productDetails.Id));

            if (product == null)
                return;

            product.Name = productDetails.Name;
            product.Price = productDetails.Price;

            DomainApprover.SaveChanges();
        }
    }
}
