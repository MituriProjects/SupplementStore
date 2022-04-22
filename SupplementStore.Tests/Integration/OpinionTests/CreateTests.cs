using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OpinionTests {

    [TestClass]
    public class CreateTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Opinion/Create");

            ExamineAuthRedirect("/Opinion/Create");
        }

        [TestMethod]
        public async Task Get_UserIsLoggedIn_ReturnsPurchaseDetails() {

            var products = TestEntity.Random<Product>(2);
            var orders = TestEntity.Random<Order>(2);
            orders[0]
                .WithUserId(TestData.User);
            orders[1]
                .WithUserId(TestData.User);
            var purchases = TestEntity.Random<Purchase>(3);
            purchases[0]
                .WithOrderId(orders[0])
                .WithProductId(products[0]);
            purchases[1]
                .WithOrderId(orders[0])
                .WithProductId(products[1]);
            purchases[2]
                .WithOrderId(orders[1])
                .WithProductId(products[1])
                .WithOpinionId(null);

            await GetAsync("/Opinion/Create", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("PurchaseId", purchases[2].PurchaseId)
                .Contains("ProductName", products[1].Name)
                .Contains("BuyingDate", orders[1].CreatedOn));
        }

        [TestMethod]
        public async Task Get_NoProductToOpine_RedirectsToIndex() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product);

            await GetAsync("/Opinion/Create", TestData.User);

            ExamineRedirect("/Opinion");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            await PostAsync("/Opinion/Create", new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "Text", "OpinionText" },
                { "Stars", "3" }
            });

            ExamineAuthRedirect("/Opinion/Create");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_RedirectsToCreate() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", purchase.PurchaseId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "3" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            ExamineRedirect("/Opinion/Create");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_CreatesOpinion() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", purchase.PurchaseId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            var createdOpinion = TestDocument<Opinion>.First(e => e.Text == "OpinionText" && e.Rating.Stars == 2 && e.PurchaseId == purchase.PurchaseId);
            TestDocument<Purchase>.Single(e => e.PurchaseId == purchase.PurchaseId && e.OpinionId == createdOpinion.OpinionId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_PurchaseDoesNotExist_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", Guid.NewGuid().ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Rating.Stars == 2);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_InvalidPurchaseId_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", "InvalidPurchaseId" },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Rating.Stars == 2);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_PurchaseHasOpinion_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var opinion = TestEntity.Random<Opinion>();
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(opinion);
            var purchaseOpinionId = purchase.OpinionId;

            var formData = new Dictionary<string, string> {
                { "PurchaseId", purchase.PurchaseId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Rating.Stars == 2 && e.PurchaseId == purchase.PurchaseId);
            Assert.AreEqual(purchaseOpinionId, purchase.OpinionId, "Purchase's OpinionId has changed.");
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_0Stars_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", purchase.PurchaseId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "0" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            ExamineExceptionThrown<InvalidStateException>();
            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Rating.Stars == 0 && e.PurchaseId == purchase.PurchaseId);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_6Stars_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User);
            var purchase = TestEntity.Random<Purchase>()
                .WithOrderId(order)
                .WithProductId(product)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "PurchaseId", purchase.PurchaseId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "6" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            ExamineExceptionThrown<InvalidStateException>();
            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Rating.Stars == 6 && e.PurchaseId == purchase.PurchaseId);
            TestDocumentApprover.ExamineNoChangesSaved();
        }
    }
}
