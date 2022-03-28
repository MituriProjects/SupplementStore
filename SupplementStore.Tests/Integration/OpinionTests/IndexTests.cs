using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OpinionTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Opinion");

            ExamineAuthRedirect("/Opinion");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsOpinionDetails() {

            var products = TestEntity.Random<Product>(3);
            var orders = TestEntity.Random<Order>(2);
            orders[0]
                .WithUserId(TestData.User.Id);
            orders[1]
                .WithUserId(TestData.User.Id);
            var purchases = TestEntity.Random<Purchase>(3);
            purchases[0]
                .WithOrderId(orders[0])
                .WithProductId(products[0]);
            purchases[1]
                .WithOrderId(orders[0])
                .WithProductId(products[1]);
            purchases[2]
                .WithOrderId(orders[1])
                .WithProductId(products[2])
                .WithOpinionId(null);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithRating(new Rating(2))
                .WithPurchaseId(purchases[0]);
            opinions[1]
                .WithRating(new Rating(3))
                .WithPurchaseId(purchases[1]);
            purchases[0]
                .WithOpinionId(opinions[0]);
            purchases[1]
                .WithOpinionId(opinions[1]);

            await GetAsync("/Opinion", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("IsProductToOpineWaiting", true)
                .Contains("ProductName", products[0].Name)
                .Contains("Stars", opinions[0].Rating.Stars)
                .Contains("Text", opinions[0].Text)
                .Contains("ProductName", products[1].Name)
                .Contains("Stars", opinions[1].Rating.Stars)
                .Contains("Text", opinions[1].Text)
                .Contains("BuyingDate", orders[0].CreatedOn)
                .Lacks("ProductName", products[2].Name)
                .Lacks("BuyingDate", orders[1].CreatedOn));
        }
    }
}
