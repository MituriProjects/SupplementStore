using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class SetImageAsMainTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", "TestImageName" }
            };

            await PostAsync("/Product/SetImageAsMain", formData);

            ExamineAuthRedirect($"/Product/SetImageAsMain");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", "TestImageName" }
            };

            await PostAsync("/Product/SetImageAsMain", formData, TestData.User);

            ExamineAccessDeniedRedirect($"/Product/SetImageAsMain");
        }

        [TestMethod]
        public async Task UserIsAdmin_RedirectsToDetails() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/SetImageAsMain", formData, TestData.Admin);

            ExamineRedirect($"/Product/Details/{formData["ProductId"]}");
        }

        [TestMethod]
        public async Task ProductImageIsNotMain_SetsProductImageAsMain() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product)
                .WithIsMain(false);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/SetImageAsMain", formData, TestData.Admin);

            TestDocument<ProductImage>.Single(e => e.Name == productImage.Name && e.IsMain == true);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task InvalidProductImage_NoExceptionThrown() {

            var product = TestEntity.Random<Product>();

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", "InvalidProductImageName" }
            };

            await PostAsync("/Product/SetImageAsMain", formData, TestData.Admin);

            ExamineNoExceptionThrown();
        }

        [TestMethod]
        public async Task AnotherProductImageIsMain_SetsAbotherProductImageAsNotMain() {

            var product = TestEntity.Random<Product>();
            var productImages = TestEntity.Random<ProductImage>(2);
            productImages[0]
                .WithProductId(product)
                .WithIsMain(false);
            productImages[1]
                .WithProductId(product)
                .WithIsMain(true);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", productImages[0].Name }
            };

            await PostAsync("/Product/SetImageAsMain", formData, TestData.Admin);

            TestDocument<ProductImage>.Single(e => e.Name == productImages[0].Name && e.IsMain == true);
            TestDocument<ProductImage>.Single(e => e.Name == productImages[1].Name && e.IsMain == false);
            TestDocumentApprover.ExamineSaveChanges();
        }
    }
}
