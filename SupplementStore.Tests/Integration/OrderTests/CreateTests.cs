using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Orders;
using SupplementStore.Domain.Entities.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OrderTests {

    [TestClass]
    public class CreateTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Order/Create");

            ExamineAuthRedirect("/Order/Create");
        }

        [TestMethod]
        public async Task Get_UserIsLoggedIn_ReturnsBasketProductDetails() {

            var products = TestEntity.Random<Product>(3);
            var basketProducts = TestEntity.Random<BasketProduct>(3);
            basketProducts[0]
                .WithProductId(products[2].Id)
                .WithUserId(TestData.User.Id);
            basketProducts[1]
                .WithProductId(products[0].Id)
                .WithUserId(TestData.User.Id);
            basketProducts[2]
                .WithProductId(products[1].Id);

            await GetAsync("/Order/Create", TestData.User);

            var contentScheme = ContentScheme.Html();
            foreach (var basketProduct in basketProducts) {

                var product = products.First(e => e.Id == basketProduct.ProductId);

                var values = new Dictionary<string, object> {
                    { "Id", basketProduct.Id },
                    { "ProductId", basketProduct.ProductId },
                    { "ProductName", product.Name },
                    { "ProductPrice", product.Price },
                    { "Quantity", basketProduct.Quantity }
                };

                foreach (var value in values) {

                    if (basketProduct.UserId == TestData.User.Id) {

                        contentScheme.Contains(value.Key, value.Value);
                    }
                    else {

                        contentScheme.Lacks(value.Key, value.Value);
                    }
                }
            }

            Examine(contentScheme);
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            await PostAsync("/Order/Create", new Dictionary<string, string>());

            ExamineAuthRedirect("/Order/Create");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_CreatesOrderAndOrderProductsAndDeletesBasketProducts() {

            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0].WithUserId(TestData.User.Id);
            basketProducts[1].WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Fircowskiego 5/7" },
                { "PostalCode", "35-030" },
                { "City", "Rzeszów" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Order>.Single(e => e.Address.Street == formData["Address"] && e.Address.PostalCode == formData["PostalCode"] && e.Address.City == formData["City"] && e.UserId == TestData.User.Id);
            var createdOrder = TestDocument<Order>.First(e => e.UserId == TestData.User.Id);
            TestDocument<OrderProduct>.Single(e => e.OrderId == createdOrder.Id && e.ProductId == basketProducts[0].ProductId && e.Quantity == basketProducts[0].Quantity);
            TestDocument<OrderProduct>.Single(e => e.OrderId == createdOrder.Id && e.ProductId == basketProducts[1].ProductId && e.Quantity == basketProducts[1].Quantity);
            TestDocument<BasketProduct>.None(e => e.Id == basketProducts[0].Id);
            TestDocument<BasketProduct>.None(e => e.Id == basketProducts[1].Id);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_RedirectsToOrderSummary() {

            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0].WithUserId(TestData.User.Id);
            basketProducts[1].WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Unii Lubelskiej 11" },
                { "PostalCode", "35-016" },
                { "City", "Rzeszów" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            var createdOrder = TestDocument<Order>.First(e => e.UserId == TestData.User.Id);
            ExamineRedirect($"/Order/Summary/{createdOrder.Id}");
        }

        [TestMethod]
        public async Task Post_PostalCodeIsInvalid_NoChangesSaved() {

            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0].WithUserId(TestData.User.Id);
            basketProducts[1].WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Marii Konopnickiej 11" },
                { "PostalCode", "999-99" },
                { "City", "Kielce" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocumentApprover.ExamineNoChangesSaved();
            Examine(ContentScheme.Html()
                .Contains("Address", formData["Address"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_AddressIsEmpty_NoOrderCreated() {

            var products = TestEntity.Random<Product>(2);
            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0]
                .WithProductId(products[0].Id)
                .WithUserId(TestData.User.Id);
            basketProducts[1]
                .WithProductId(products[1].Id)
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "PostalCode", "00-046" },
                { "City", "Warszawa" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Order>.None(e => e.UserId == TestData.User.Id);
            Examine(ContentScheme.Html()
                .Contains("Address", "")
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_PostalCodeIsEmpty_NoOrderCreated() {

            var products = TestEntity.Random<Product>(2);
            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0]
                .WithProductId(products[0].Id)
                .WithUserId(TestData.User.Id);
            basketProducts[1]
                .WithProductId(products[1].Id)
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Adama Mickiewicza 66/43" },
                { "City", "Warszawa" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Order>.None(e => e.UserId == TestData.User.Id);
            Examine(ContentScheme.Html()
                .Contains("Address", formData["Address"])
                .Contains("PostalCode", "")
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_CityIsEmpty_NoOrderCreated() {

            var products = TestEntity.Random<Product>(2);
            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0]
                .WithProductId(products[0].Id)
                .WithUserId(TestData.User.Id);
            basketProducts[1]
                .WithProductId(products[1].Id)
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Dworcowa 33/65" },
                { "PostalCode", "10-437" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Order>.None(e => e.UserId == TestData.User.Id);
            Examine(ContentScheme.Html()
                .Contains("Address", formData["Address"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", ""));
        }
    }
}
