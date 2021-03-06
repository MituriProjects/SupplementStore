using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OpinionTests {

    [TestClass]
    public class HideTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var opinionId = Guid.NewGuid();

            await PostAsync($"/Opinion/Hide/{opinionId}");

            ExamineAuthRedirect($"/Opinion/Hide/{opinionId}");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            var opinionId = Guid.NewGuid();

            await PostAsync($"/Opinion/Hide/{opinionId}", TestData.User);

            ExamineAccessDeniedRedirect($"/Opinion/Hide/{opinionId}");
        }

        [TestMethod]
        public async Task UserIsAuthorized_RedirectsToProductDetails() {

            var product = TestEntity.Random<Product>();
            var purchase = TestEntity.Random<Purchase>();
            var opinion = TestEntity.Random<Opinion>()
                .WithPurchaseId(purchase);
            purchase
                .WithOpinionId(opinion)
                .WithProductId(product);

            await PostAsync($"/Opinion/Hide/{opinion.OpinionId}", TestData.Admin);

            ExamineRedirect($"/Product/Details/{product.ProductId}");
        }

        [TestMethod]
        public async Task OpinionIdIsValid_HidesOpinion() {

            var product = TestEntity.Random<Product>();
            var purchase = TestEntity.Random<Purchase>();
            var opinion = TestEntity.Random<Opinion>()
                .WithPurchaseId(purchase);
            purchase
                .WithOpinionId(opinion)
                .WithProductId(product);

            await PostAsync($"/Opinion/Hide/{opinion.OpinionId}", TestData.Admin);

            Assert.AreEqual(true, opinion.IsHidden);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task InvalidOpinionId_ThrowsMissingEntityException() {

            await PostAsync($"/Opinion/Hide/{Guid.NewGuid()}", TestData.Admin);

            ExamineExceptionThrown<MissingEntityException>();
        }
    }
}
