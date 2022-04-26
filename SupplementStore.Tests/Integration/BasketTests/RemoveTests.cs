using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Baskets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketTests {

    [TestClass]
    public class RemoveTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedIn_RedirectsToBasketIndex() {

            await PostAsync($"/Basket/Remove", new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            }, TestData.User);

            ExamineRedirect($"/Basket");
        }

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await PostAsync($"/Basket/Remove", new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            });

            ExamineAuthRedirect($"/Basket/Remove");
        }

        [TestMethod]
        public async Task BasketProductExists_DeletesBasketProduct() {

            var basketProduct = TestEntity.Random<BasketProduct>()
                .WithUserId(TestData.User);

            await PostAsync($"/Basket/Remove", new Dictionary<string, string> {
                { "ProductId", basketProduct.ProductId.ToString() }
            }, TestData.User);

            TestDocument<BasketProduct>.None(e => e.UserId == TestData.User.Id && e.ProductId == basketProduct.ProductId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task NoBasketProduct_NoChangesSaved() {

            await PostAsync($"/Basket/Remove", new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            }, TestData.User);

            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task InvalidProductId_NoChangesSaved() {

            await PostAsync($"/Basket/Remove", new Dictionary<string, string> {
                { "ProductId", "InvalidProductId" }
            }, TestData.User);

            TestDocumentApprover.ExamineNoChangesSaved();
        }
    }
}
