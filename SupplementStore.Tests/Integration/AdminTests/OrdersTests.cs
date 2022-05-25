using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AdminTests {

    [TestClass]
    public class OrdersTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Admin/Orders");

            ExamineAuthRedirect("/Admin/Orders");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            await GetAsync("/Admin/Orders", TestData.User);

            ExamineAccessDeniedRedirect("/Admin/Orders");
        }

        [TestMethod]
        public async Task UserIsAuthorized_ReturnsOrderDetails() {

            var products = TestEntity.Random<Product>(3);
            var addresses = TestEntity.Random<Address>(2);
            var orders = TestEntity.Random<Order>(2);
            orders[0]
                .WithUserId(TestData.Users[0])
                .WithAddressId(addresses[0]);
            orders[1]
                .WithUserId(TestData.Users[1])
                .WithAddressId(addresses[1]);
            var purchases = TestEntity.Random<Purchase>(4);
            purchases[0]
                .WithProductId(products[1])
                .WithOrderId(orders[1]);
            purchases[1]
                .WithProductId(products[2])
                .WithOrderId(orders[0]);
            purchases[2]
                .WithProductId(products[0])
                .WithOrderId(orders[0]);
            purchases[3]
                .WithProductId(products[2])
                .WithOrderId(orders[1]);

            await GetAsync("/Admin/Orders", TestData.Admin);

            var contentScheme = ContentScheme.Html();
            foreach (var order in orders) {

                contentScheme.Contains("OrderId", order.OrderId);
                contentScheme.Contains("UserId", order.UserId);
                contentScheme.Contains("UserEmail", TestData.Users.First(e => e.Id == order.UserId).Email);
                contentScheme.Contains("CreatedOn", order.CreatedOn);

                var orderAddress = addresses.First(e => e.AddressId == order.AddressId);

                contentScheme.Contains("Street", orderAddress.Street);
                contentScheme.Contains("PostalCode", orderAddress.PostalCode.Value);
                contentScheme.Contains("City", orderAddress.City);

                foreach (var purchase in purchases.Where(e => e.OrderId == order.OrderId)) {

                    contentScheme.Contains("ProductId", purchase.ProductId);
                    contentScheme.Contains("ProductName", products.First(e => e.ProductId == purchase.ProductId).Name);
                    contentScheme.Contains("ProductPrice", products.First(e => e.ProductId == purchase.ProductId).Price);
                    contentScheme.Contains("Quantity", purchase.Quantity);
                }
            }

            Examine(contentScheme);
        }

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsAllOrdersCount() {

            var orders = TestEntity.Random<Order>(3);
            orders.ToList()
                .ForEach(e => e.WithUserId(TestData.User));

            await GetAsync("/Admin/Orders", TestData.Admin);

            Examine(ContentScheme.Html()
                .Contains("AllOrdersCount", 3));
        }

        [TestMethod]
        public async Task SkipEquals2AndTakeEquals2_ReturnsDetailsOfAppropriateOrders() {

            var orders = TestEntity.Random<Order>(5);
            orders.ToList()
                .ForEach(e => e.WithUserId(TestData.User));

            await GetAsync("/Admin/Orders?Page.Skip=2&Page.Take=2", TestData.Admin);

            var contentScheme = ContentScheme.Html();
            for (int i = 0; i < orders.Count(); i++) {

                if (i < 2 || i > 3) {

                    contentScheme.Lacks("OrderId", orders[i].OrderId);
                    contentScheme.Lacks("CreatedOn", orders[i].CreatedOn);
                }
                else {

                    contentScheme.Contains("OrderId", orders[i].OrderId);
                    contentScheme.Contains("CreatedOn", orders[i].CreatedOn);
                }
            }

            Examine(contentScheme);
        }
    }
}
