using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketProductApiTests {

    [TestClass]
    public class GetTests : IntegrationTestsBase {

        [TestMethod]
        public async Task ReturnsBasketProductDetails() {

            var product = TestProduct.Random();
            var basketProduct = TestBasketProduct.Random()
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
