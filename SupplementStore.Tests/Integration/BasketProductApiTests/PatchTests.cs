using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Entities.Baskets;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketProductApiTests {

    [TestClass]
    public class PatchTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UpdatesBasketProductQuantity() {

            var product = TestEntity.Random<TestProduct>();
            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithProductId(product.Id);

            var newQuantity = basketProduct.Quantity + 3;

            await PatchAsync($"api/basketproduct/{basketProduct.Id}", new { op = "replace", path = "quantity", value = newQuantity });

            TestDocument<BasketProduct>.Single(e => e.Id == basketProduct.Id && e.Quantity == newQuantity);
            TestDocumentApprover.ExamineSaveChanges();
        }
    }
}
