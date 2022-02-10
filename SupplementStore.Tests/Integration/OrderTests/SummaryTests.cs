using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OrderTests {

    [TestClass]
    public class SummaryTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var orderId = Guid.NewGuid();

            await GetAsync($"/Order/Summary/{orderId}");

            ExamineAuthRedirect($"/Order/Summary/{orderId}");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsOrderDetails() {

            var order = TestEntity.Random<TestOrder>()
                .WithUserId(TestData.User.Id);
            var products = TestEntity.Random<TestProduct>(2);
            var orderProducts = TestEntity.Random<TestOrderProduct>(2);
            orderProducts[0]
                .WithOrderId(order.Id)
                .WithProductId(products[0].Id);
            orderProducts[1]
                .WithOrderId(order.Id)
                .WithProductId(products[1].Id);

            await GetAsync($"/Order/Summary/{order.Id}", TestData.User);

            var contentScheme = ContentScheme.Html()
                .Contains("Id", order.Id)
                .Contains("Address", order.Address.Street)
                .Contains("PostalCode", order.Address.PostalCode)
                .Contains("City", order.Address.City)
                .Contains("CreatedOn", order.CreatedOn);

            foreach (var orderProduct in orderProducts) {

                contentScheme.Contains("ProductId", orderProduct.ProductId);
                contentScheme.Contains("ProductName", products.First(e => e.Id == orderProduct.ProductId).Name);
                contentScheme.Contains("ProductPrice", products.First(e => e.Id == orderProduct.ProductId).Price);
                contentScheme.Contains("Quantity", orderProduct.Quantity);
            }

            Examine(contentScheme);
        }

        [TestMethod]
        public async Task OrderDoesNotBelongToUser_RedirectsToMain() {

            var order = TestEntity.Random<TestOrder>()
                .WithUserId(TestData.Users[1].Id);

            await GetAsync($"/Order/Summary/{order.Id}", TestData.Users[0]);

            ExamineRedirect("/");
        }

        [TestMethod]
        public async Task OrderDoesNotExist_RedirectsToMain() {

            await GetAsync($"/Order/Summary/{Guid.NewGuid()}", TestData.User);

            ExamineRedirect("/");
        }
    }
}
