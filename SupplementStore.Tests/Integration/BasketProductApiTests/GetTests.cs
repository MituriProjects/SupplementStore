using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketProductApiTests {

    [TestClass]
    public class GetTests : IntegrationTestsBase {

        [TestMethod]
        public async Task ReturnsBasketProductDetails() {

            var product = TestEntity.Random<Product>();
            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithProductId(product.Id);

            await GetAsync($"api/basketproduct/{basketProduct.Id}");

            Examine(ContentScheme.Json()
                .Contains("Id", basketProduct.Id)
                .Contains("ProductId", basketProduct.ProductId)
                .Contains("ProductName", product.Name)
                .Contains("ProductPrice", product.Price)
                .Contains("Quantity", basketProduct.Quantity));
        }
    }
}
