using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OpinionTests {

    [TestClass]
    public class EditTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            var opinionId = Guid.NewGuid();

            await GetAsync($"/Opinion/Edit/{opinionId}");

            ExamineAuthRedirect($"/Opinion/Edit/{opinionId}");
        }

        [TestMethod]
        public async Task Get_UserIsNotAuthorized_AccessDenied() {

            var opinionId = Guid.NewGuid();

            await GetAsync($"/Opinion/Edit/{opinionId}", TestData.User);

            ExamineAccessDeniedRedirect($"/Opinion/Edit/{opinionId}");
        }

        [TestMethod]
        public async Task Get_UserIsAuthorized_ReturnsOpinionDetails() {

            var product = TestEntity.Random<Product>();
            var order = TestEntity.Random<Order>();
            var orderProduct = TestEntity.Random<OrderProduct>();
            var opinion = TestEntity.Random<Opinion>()
                .WithOrderProductId(orderProduct.OrderProductId);
            orderProduct
                .WithOpinionId(opinion.OpinionId)
                .WithOrderId(order.OrderId)
                .WithProductId(product.ProductId);

            await GetAsync($"/Opinion/Edit/{opinion.OpinionId}", TestData.Admin);

            Examine(ContentScheme.Html()
                .Contains("Id", opinion.OpinionId)
                .Contains("Text", opinion.Text));
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Text", "OpinionText" }
            };

            await PostAsync("/Opinion/Edit", formData);

            ExamineAuthRedirect("/Opinion/Edit");
        }

        [TestMethod]
        public async Task Post_UserIsNotAuthorized_AccessDenied() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Text", "OpinionText" }
            };

            await PostAsync("/Opinion/Edit", formData, TestData.User);

            ExamineAccessDeniedRedirect("/Opinion/Edit");
        }

        [TestMethod]
        public async Task Post_UserIsAuthorized_RedirectsToProductDetails() {

            var product = TestEntity.Random<Product>();
            var orderProduct = TestEntity.Random<OrderProduct>();
            var opinion = TestEntity.Random<Opinion>()
                .WithOrderProductId(orderProduct.OrderProductId);
            orderProduct
                .WithOpinionId(opinion.OpinionId)
                .WithProductId(product.ProductId);

            var formData = new Dictionary<string, string> {
                { "Id", opinion.OpinionId.ToString() },
                { "Text", "OpinionText" }
            };

            await PostAsync("/Opinion/Edit", formData, TestData.Admin);

            ExamineRedirect($"/Product/Details/{product.ProductId}");
        }

        [TestMethod]
        public async Task Post_OpinionIdIsValid_UpdatesOpinionText() {

            var product = TestEntity.Random<Product>();
            var orderProduct = TestEntity.Random<OrderProduct>();
            var opinion = TestEntity.Random<Opinion>()
                .WithOrderProductId(orderProduct.OrderProductId);
            orderProduct
                .WithOpinionId(opinion.OpinionId)
                .WithProductId(product.ProductId);

            var formData = new Dictionary<string, string> {
                { "Id", opinion.OpinionId.ToString() },
                { "Text", "NewOpinionText" }
            };

            await PostAsync("/Opinion/Edit", formData, TestData.Admin);

            Assert.AreEqual(formData["Text"], opinion.Text);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_InvalidOpinionId_ThrowsMissingEntityException() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Text", "NewOpinionText" }
            };

            await PostAsync("/Opinion/Edit", formData, TestData.Admin);

            ExamineExceptionThrown<MissingEntityException>();
        }
    }
}
