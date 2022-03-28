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
            var purchases = TestEntity.Random<Purchase>(3);
            var opinions = TestEntity.Random<Opinion>(3);
            opinions[0]
                .WithPurchaseId(purchases[0])
                .WithIsHidden(true);
            opinions[1]
                .WithPurchaseId(purchases[1])
                .WithIsHidden(false);
            opinions[2]
                .WithPurchaseId(purchases[2])
                .WithIsHidden(true);
            purchases[0]
                .WithOpinionId(opinions[0])
                .WithProductId(products[0]);
            purchases[1]
                .WithOpinionId(opinions[1])
                .WithProductId(products[1]);
            purchases[2]
                .WithOpinionId(opinions[2])
                .WithProductId(products[1]);

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
