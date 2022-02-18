using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketProductApiTests {

    [TestClass]
    public class GetTests : IntegrationTestsBase {

        [TestMethod]
        public async Task ReturnsBasketProductDetails() {

            var product = TestEntity.Random<Product>();
            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithProductId(product.ProductId);

            await GetAsync($"api/basketproduct/{basketProduct.BasketProductId}");

            Examine(ContentScheme.Json()
                .Contains("Id", basketProduct.BasketProductId)
                .Contains("ProductId", basketProduct.ProductId)
                .Contains("ProductName", product.Name)
                .Contains("ProductPrice", product.Price)
                .Contains("Quantity", basketProduct.Quantity));
        }
    }
}
