using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SupplementStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class RemoveImageTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", "TestImageName" }
            };

            await PostAsync("/Product/RemoveImage", formData);

            ExamineAuthRedirect($"/Product/RemoveImage");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", "TestImageName" }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.User);

            ExamineAccessDeniedRedirect($"/Product/RemoveImage");
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

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            ExamineRedirect($"/Product/Details/{formData["ProductId"]}");
        }

        [TestMethod]
        public async Task ValidProductImage_DeletesProductImage() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            TestDocument<ProductImage>.None(e => e.ProductId == product.ProductId && e.Name == productImage.Name);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task ValidProductImage_DeletesFile() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.Delete(productImage.Name, "productImages", formData["ProductId"]), Times.Once);
        }

        [TestMethod]
        public async Task InvalidProductId_NoExceptionThrown() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            ExamineNoExceptionThrown();
        }

        [TestMethod]
        public async Task InvalidProductId_NoFileDeletion() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() },
                { "ImageName", productImage.Name }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.Delete(productImage.Name, "productImages", formData["ProductId"]), Times.Never);
        }

        [TestMethod]
        public async Task InvalidProductImageName_NoExceptionThrown() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", "InvalidImageName" }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            ExamineNoExceptionThrown();
        }

        [TestMethod]
        public async Task InvalidProductImageName_NoFileDeletion() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() },
                { "ImageName", "InvalidImageName" }
            };

            await PostAsync("/Product/RemoveImage", formData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.Delete(formData["ImageName"], "productImages", formData["ProductId"]), Times.Never);
        }
    }
}
