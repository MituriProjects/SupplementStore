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
            var orderProducts = TestEntity.Random<OrderProduct>(3);
            orderProducts[0]
                .WithOrderId(orders[0].OrderId)
                .WithProductId(products[0].ProductId);
            orderProducts[1]
                .WithOrderId(orders[0].OrderId)
                .WithProductId(products[1].ProductId);
            orderProducts[2]
                .WithOrderId(orders[1].OrderId)
                .WithProductId(products[2].ProductId)
                .WithOpinionId(null);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithGrade(new Grade(2))
                .WithOrderProductId(orderProducts[0]);
            opinions[1]
                .WithGrade(new Grade(3))
                .WithOrderProductId(orderProducts[1]);
            orderProducts[0]
                .WithOpinionId(opinions[0].OpinionId);
            orderProducts[1]
                .WithOpinionId(opinions[1].OpinionId);

            await GetAsync("/Opinion", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("IsProductToOpineWaiting", true)
                .Contains("ProductName", products[0].Name)
                .Contains("Stars", opinions[0].Grade.Stars)
                .Contains("Text", opinions[0].Text)
                .Contains("ProductName", products[1].Name)
                .Contains("Stars", opinions[1].Grade.Stars)
                .Contains("Text", opinions[1].Text)
                .Contains("BuyingDate", orders[0].CreatedOn)
                .Lacks("ProductName", products[2].Name)
                .Lacks("BuyingDate", orders[1].CreatedOn));
        }
    }
}
