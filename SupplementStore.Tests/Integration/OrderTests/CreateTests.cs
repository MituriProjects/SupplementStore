using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
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
                .WithProductId(products[2].ProductId)
                .WithUserId(TestData.User);
            basketProducts[1]
                .WithProductId(products[0].ProductId)
                .WithUserId(TestData.User);
            basketProducts[2]
                .WithProductId(products[1].ProductId);

            await GetAsync("/Order/Create", TestData.User);

            var contentScheme = ContentScheme.Html();
            foreach (var basketProduct in basketProducts) {

                var product = products.First(e => e.ProductId == basketProduct.ProductId);

                var values = new Dictionary<string, object> {
                    { "Id", basketProduct.BasketProductId },
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
        public async Task Post_UserIsLoggedIn_CreatesOrderAndPurchasesAndDeletesBasketProducts() {

            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0].WithUserId(TestData.User);
            basketProducts[1].WithUserId(TestData.User);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Fircowskiego 5/7" },
                { "PostalCode", "35-030" },
                { "City", "Rzeszów" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            var createdAddress = TestDocument<Address>.First(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id);
            var createdOrder = TestDocument<Order>.First(e => e.UserId == TestData.User.Id && e.AddressId == createdAddress.AddressId);
            TestDocument<Purchase>.Single(e => e.OrderId == createdOrder.OrderId && e.ProductId == basketProducts[0].ProductId && e.Quantity == basketProducts[0].Quantity);
            TestDocument<Purchase>.Single(e => e.OrderId == createdOrder.OrderId && e.ProductId == basketProducts[1].ProductId && e.Quantity == basketProducts[1].Quantity);
            TestDocument<BasketProduct>.None(e => e.BasketProductId == basketProducts[0].BasketProductId);
            TestDocument<BasketProduct>.None(e => e.BasketProductId == basketProducts[1].BasketProductId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_UserAddressExists_CreatesOrderWithExistingAddress() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.User);
            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User)
                .WithStreet("Lotnicza 39/4")
                .WithPostalCode(new PostalCode("22-726"))
                .WithCity("Legnica");

            var formData = new Dictionary<string, string> {
                { "Address", address.Street },
                { "PostalCode", address.PostalCode.Value },
                { "City", address.City }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id);
            TestDocument<Order>.Single(e => e.UserId == TestData.User.Id && e.AddressId == address.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_AddressDoesNotExistAndIsNotToBeSaved_CreatesOrderAndSetsAddressAsHidden() {

            var basketProduct = TestEntity.Random<BasketProduct>()
               .WithUserId(TestData.User);

            var formData = new Dictionary<string, string> {
                { "Address", "Rycerska 11/5" },
                { "PostalCode", "11-459" },
                { "City", "Leszno" },
                { "IsAddressToBeSaved", "false" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id
                && e.IsHidden);
            var createdAddress = TestDocument<Address>.First(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id
                && e.IsHidden);
            TestDocument<Order>.Single(e => e.UserId == TestData.User.Id && e.AddressId == createdAddress.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_UserAddressDoesNotExist_CreatesOrderAndAddress() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.Users[0]);
            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.Users[1])
                .WithStreet("Lotnicza 39/4")
                .WithPostalCode(new PostalCode("22-726"))
                .WithCity("Legnica");

            var formData = new Dictionary<string, string> {
                { "Address", address.Street },
                { "PostalCode", address.PostalCode.Value },
                { "City", address.City }
            };

            await PostAsync("/Order/Create", formData, TestData.Users[0]);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.Users[1].Id);
            var createdAddress = TestDocument<Address>.First(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.Users[0].Id);
            TestDocument<Order>.Single(e => e.UserId == TestData.Users[0].Id && e.AddressId == createdAddress.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_AddressIsHiddenAndIsToBeSaved_CreatesOrderAndSetsAddressAsNotHidden() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.User);
            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User)
                .WithStreet("Hutnicza 34/10")
                .WithPostalCode(new PostalCode("14-141"))
                .WithCity("Gniezno")
                .WithIsHidden(true);

            var formData = new Dictionary<string, string> {
                { "Address", address.Street },
                { "PostalCode", address.PostalCode.Value },
                { "City", address.City },
                { "IsAddressToBeSaved", "true" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id
                && e.IsHidden == false);
            TestDocument<Order>.Single(e => e.UserId == TestData.User.Id && e.AddressId == address.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_AddressIsHiddenAndIsNotToBeSaved_CreatesOrderAndLeavesAddressAsHidden() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.User);
            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User)
                .WithStreet("Karpacka 6/12")
                .WithPostalCode(new PostalCode("55-113"))
                .WithCity("Walcz")
                .WithIsHidden(true);

            var formData = new Dictionary<string, string> {
                { "Address", address.Street },
                { "PostalCode", address.PostalCode.Value },
                { "City", address.City },
                { "IsAddressToBeSaved", "false" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id
                && e.IsHidden == true);
            TestDocument<Order>.Single(e => e.UserId == TestData.User.Id && e.AddressId == address.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_AddressIsNotHiddenAndIsNotToBeSaved_CreatesOrderAndLeavesAddressAsNotHidden() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.User);
            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User)
                .WithStreet("Kolonijna 2/14")
                .WithPostalCode(new PostalCode("11-736"))
                .WithCity("Gryfice")
                .WithIsHidden(false);

            var formData = new Dictionary<string, string> {
                { "Address", address.Street },
                { "PostalCode", address.PostalCode.Value },
                { "City", address.City },
                { "IsAddressToBeSaved", "false" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.Street == formData["Address"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.UserId == TestData.User.Id
                && e.IsHidden == false);
            TestDocument<Order>.Single(e => e.UserId == TestData.User.Id && e.AddressId == address.AddressId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_RedirectsToOrderSummary() {

            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0].WithUserId(TestData.User);
            basketProducts[1].WithUserId(TestData.User);

            var formData = new Dictionary<string, string> {
                { "Address", "ul. Unii Lubelskiej 11" },
                { "PostalCode", "35-016" },
                { "City", "Rzeszów" }
            };

            await PostAsync("/Order/Create", formData, TestData.User);

            var createdOrder = TestDocument<Order>.First(e => e.UserId == TestData.User.Id);
            ExamineRedirect($"/Order/Summary/{createdOrder.OrderId}");
        }

        [TestMethod]
        public async Task Post_PostalCodeIsInvalid_NoChangesSaved() {

            var products = TestEntity.Random<Product>(2);
            var basketProducts = TestEntity.Random<BasketProduct>(2);
            basketProducts[0]
                .WithProductId(products[0].ProductId)
                .WithUserId(TestData.User);
            basketProducts[1]
                .WithProductId(products[1].ProductId)
                .WithUserId(TestData.User);

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
                .WithProductId(products[0].ProductId)
                .WithUserId(TestData.User);
            basketProducts[1]
                .WithProductId(products[1].ProductId)
                .WithUserId(TestData.User);

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
                .WithProductId(products[0].ProductId)
                .WithUserId(TestData.User);
            basketProducts[1]
                .WithProductId(products[1].ProductId)
                .WithUserId(TestData.User);

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
                .WithProductId(products[0].ProductId)
                .WithUserId(TestData.User);
            basketProducts[1]
                .WithProductId(products[1].ProductId)
                .WithUserId(TestData.User);

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
