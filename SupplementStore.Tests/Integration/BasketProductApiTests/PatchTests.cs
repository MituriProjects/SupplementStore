using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketProductApiTests {

    [TestClass]
    public class PatchTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UpdatesBasketProductQuantity() {

            var product = TestEntity.Random<Product>();
            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithProductId(product.ProductId);

            var newQuantity = basketProduct.Quantity + 3;

            await PatchAsync($"api/basketproduct/{basketProduct.BasketProductId}", new { op = "replace", path = "quantity", value = newQuantity });

            TestDocument<BasketProduct>.Single(e => e.BasketProductId == basketProduct.BasketProductId && e.Quantity == newQuantity);
            TestDocumentApprover.ExamineSaveChanges();
        }
    }
}
