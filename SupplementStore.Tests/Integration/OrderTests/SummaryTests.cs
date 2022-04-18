using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
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

            var address = TestEntity.Random<Address>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id)
                .WithAddressId(address);
            var products = TestEntity.Random<Product>(2);
            var purchases = TestEntity.Random<Purchase>(2);
            purchases[0]
                .WithOrderId(order)
                .WithProductId(products[0]);
            purchases[1]
                .WithOrderId(order)
                .WithProductId(products[1]);

            await GetAsync($"/Order/Summary/{order.OrderId}", TestData.User);

            var contentScheme = ContentScheme.Html()
                .Contains("Id", order.OrderId)
                .Contains("Address", address.Street)
                .Contains("PostalCode", address.PostalCode.Value)
                .Contains("City", address.City)
                .Contains("CreatedOn", order.CreatedOn);

            foreach (var purchase in purchases) {

                contentScheme.Contains("ProductId", purchase.ProductId);
                contentScheme.Contains("ProductName", products.First(e => e.ProductId == purchase.ProductId).Name);
                contentScheme.Contains("ProductPrice", products.First(e => e.ProductId == purchase.ProductId).Price);
                contentScheme.Contains("Quantity", purchase.Quantity);
            }

            Examine(contentScheme);
        }

        [TestMethod]
        public async Task OrderDoesNotBelongToUser_RedirectsToMain() {

            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.Users[1].Id);

            await GetAsync($"/Order/Summary/{order.OrderId}", TestData.Users[0]);

            ExamineRedirect("/");
        }

        [TestMethod]
        public async Task OrderDoesNotExist_RedirectsToMain() {

            await GetAsync($"/Order/Summary/{Guid.NewGuid()}", TestData.User);

            ExamineRedirect("/");
        }
    }
}
