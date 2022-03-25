using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var products = TestEntity.Random<Product>(2);
            var orderProducts = TestEntity.Random<OrderProduct>(2);
            orderProducts[0]
                .WithOrderId(order)
                .WithProductId(products[0].ProductId);
            orderProducts[1]
                .WithOrderId(order)
                .WithProductId(products[1].ProductId);

            await GetAsync($"/Order/Summary/{order.OrderId}", TestData.User);

            var contentScheme = ContentScheme.Html()
                .Contains("Id", order.OrderId)
                .Contains("Address", order.Address.Street)
                .Contains("PostalCode", order.Address.PostalCode)
                .Contains("City", order.Address.City)
                .Contains("CreatedOn", order.CreatedOn);

            foreach (var orderProduct in orderProducts) {

                contentScheme.Contains("ProductId", orderProduct.ProductId);
                contentScheme.Contains("ProductName", products.First(e => e.ProductId == orderProduct.ProductId).Name);
                contentScheme.Contains("ProductPrice", products.First(e => e.ProductId == orderProduct.ProductId).Price);
                contentScheme.Contains("Quantity", orderProduct.Quantity);
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
