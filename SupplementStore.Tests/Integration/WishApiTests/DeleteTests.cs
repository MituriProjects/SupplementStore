using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.WishApiTests {

    [TestClass]
    public class DeleteTests : IntegrationTestsBase {

        [TestMethod]
        public async Task ValidProductId_DeletesWish() {

            var product = TestEntity.Random<Product>();
            var wish = TestEntity.Random<Wish>();
            wish
                .WithProductId(product)
                .WithUserId(TestData.User.Id);

            await DeleteAsync($"api/wish/{product.ProductId}", TestData.User);

            TestDocument<Wish>.None(e => e.UserId == TestData.User.Id && e.ProductId == product.ProductId);
            TestDocumentApprover.ExamineSaveChanges();
        }
    }
}
