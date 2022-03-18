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
        public async Task Get_UserIsLoggedIn_ReturnsOrderProductDetails() {

            var products = TestEntity.Random<Product>(2);
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
                .WithProductId(products[1].ProductId)
                .WithOpinionId(null);

            await GetAsync("/Opinion/Create", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("OrderProductId", orderProducts[2].OrderProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("BuyingDate", orders[1].CreatedOn));
        }

        [TestMethod]
        public async Task Get_NoProductToOpine_RedirectsToIndex() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId);

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
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", orderProduct.OrderProductId.ToString() },
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
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", orderProduct.OrderProductId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            var createdOpinion = TestDocument<Opinion>.First(e => e.Text == "OpinionText" && e.Grade.Stars == 2 && e.OrderProductId == orderProduct.OrderProductId);
            TestDocument<OrderProduct>.Single(e => e.OrderProductId == orderProduct.OrderProductId && e.OpinionId == createdOpinion.OpinionId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_OrderProductDoesNotExist_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", Guid.NewGuid().ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Grade.Stars == 2);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_InvalidOrderProductId_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", "InvalidOrderProductId" },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Grade.Stars == 2);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_OrderProductHasOpinion_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var opinion = TestEntity.Random<Opinion>();
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(opinion.OpinionId);
            var orderProductOpinionId = orderProduct.OpinionId;

            var formData = new Dictionary<string, string> {
                { "OrderProductId", orderProduct.OrderProductId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "2" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Grade.Stars == 2 && e.OrderProductId == orderProduct.OrderProductId);
            Assert.AreEqual(orderProductOpinionId, orderProduct.OpinionId, "OrderProduct's OpinionId has changed.");
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_0Stars_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", orderProduct.OrderProductId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "0" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            ExamineExceptionThrown<InvalidStateException>();
            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Grade.Stars == 0 && e.OrderProductId == orderProduct.OrderProductId);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_6Stars_NoOpinionCreation() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>()
                .WithUserId(TestData.User.Id);
            var orderProduct = TestEntity.Random<OrderProduct>()
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId)
                .WithOpinionId(null);

            var formData = new Dictionary<string, string> {
                { "OrderProductId", orderProduct.OrderProductId.ToString() },
                { "Text", "OpinionText" },
                { "Stars", "6" }
            };

            await PostAsync("/Opinion/Create", formData, TestData.User);

            ExamineExceptionThrown<InvalidStateException>();
            TestDocument<Opinion>.None(e => e.Text == "OpinionText" && e.Grade.Stars == 6 && e.OrderProductId == orderProduct.OrderProductId);
            TestDocumentApprover.ExamineNoChangesSaved();
        }
    }
}
