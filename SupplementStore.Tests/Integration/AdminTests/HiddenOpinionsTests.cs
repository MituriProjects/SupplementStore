using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AdminTests {

    [TestClass]
    public class HiddenOpinionsTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Admin/HiddenOpinions");

            ExamineAuthRedirect("/Admin/HiddenOpinions");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            await GetAsync("/Admin/HiddenOpinions", TestData.User);

            ExamineAccessDeniedRedirect("/Admin/HiddenOpinions");
        }

        [TestMethod]
        public async Task UserIsAuthorized_ReturnsHiddenOpiniondetails() {

            var products = TestEntity.Random<Product>(2);
            var orderProducts = TestEntity.Random<OrderProduct>(3);
            var opinions = TestEntity.Random<Opinion>(3);
            opinions[0]
                .WithOrderProductId(orderProducts[0])
                .WithIsHidden(true);
            opinions[1]
                .WithOrderProductId(orderProducts[1])
                .WithIsHidden(false);
            opinions[2]
                .WithOrderProductId(orderProducts[2])
                .WithIsHidden(true);
            orderProducts[0]
                .WithOpinionId(opinions[0].OpinionId)
                .WithProductId(products[0].ProductId);
            orderProducts[1]
                .WithOpinionId(opinions[1].OpinionId)
                .WithProductId(products[1].ProductId);
            orderProducts[2]
                .WithOpinionId(opinions[2].OpinionId)
                .WithProductId(products[1].ProductId);

            await GetAsync("/Admin/HiddenOpinions", TestData.Admin);

            Examine(ContentScheme.Html()
                .Contains("Id", opinions[0].OpinionId)
                .Contains("ProductName", products[0].Name)
                .Contains("Text", opinions[0].Text)
                .Lacks("Id", opinions[1].OpinionId)
                .Lacks("Text", opinions[1].Text)
                .Contains("Id", opinions[2].OpinionId)
                .Contains("ProductName", products[1].Name)
                .Contains("Text", opinions[2].Text));
        }
    }
}
