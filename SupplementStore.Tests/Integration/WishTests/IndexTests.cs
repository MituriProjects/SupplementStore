using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.WishTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Wish");

            ExamineAuthRedirect("/Wish");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsWishedProductDetails() {

            var products = TestEntity.Random<Product>(3);
            var wishes = TestEntity.Random<Wish>(3);
            wishes[0]
                .WithProductId(products[2])
                .WithUserId(TestData.User.Id);
            wishes[2]
                .WithProductId(products[1])
                .WithUserId(TestData.User.Id);

            await GetAsync("/Wish", TestData.User);

            Examine(ContentScheme.Html()
                .Lacks("ProductId", products[0].ProductId)
                .Lacks("ProductName", products[0].Name)
                .Lacks("ProductPrice", products[0].Price)
                .Contains("ProductId", products[2].ProductId)
                .Contains("ProductName", products[2].Name)
                .Contains("ProductPrice", products[2].Price)
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price));
        }
    }
}
