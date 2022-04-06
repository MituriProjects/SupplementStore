using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SupplementStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class AddImageTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData);

            ExamineAuthRedirect($"/Product/AddImage");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.User);

            ExamineAccessDeniedRedirect("/Product/AddImage");
        }

        [TestMethod]
        public async Task UserIsAdmin_RedirectsToDetails() {

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            ExamineRedirect($"/Product/Details/{formData["ProductId"]}");
        }

        [TestMethod]
        public async Task ValidProductId_CreatesProductImage() {

            var product = TestEntity.Random<Product>();

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            TestDocument<ProductImage>.Single(e => e.ProductId == new ProductId(formData["ProductId"]) && e.Name == formFileData.FileName);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task ValidProductId_ProcessesFileDataByFileWriter() {

            var product = TestEntity.Random<Product>();

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.ProcessAsync(It.Is<IFormFile>(f => f.FileName == "test.txt"), "productImages", formData["ProductId"]), Times.Once);
        }

        [TestMethod]
        public async Task InvalidProductId_NoProductImageCreation() {

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            TestDocument<ProductImage>.None(e => e.ProductId == new ProductId(formData["ProductId"]) && e.Name == formFileData.FileName);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task InvalidProductId_NoFileDataProcessingByFileWriter() {

            var formFileData = new FormFileData("This is a test file", "File", "test.txt");
            var formData = new Dictionary<string, string> {
                { "ProductId", Guid.NewGuid().ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.ProcessAsync(It.Is<IFormFile>(f => f.FileName == "test.txt"), "productImages", formData["ProductId"]), Times.Never);
        }

        [TestMethod]
        public async Task ProductImageExists_NoProductImageCreation() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formFileData = new FormFileData("This is a test file", "File", productImage.Name);
            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            TestDocument<ProductImage>.Single(e => e.ProductId == new ProductId(formData["ProductId"]) && e.Name == formFileData.FileName);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task ProductImageExists_NoFileDataProcessingByFileWriter() {

            var product = TestEntity.Random<Product>();
            var productImage = TestEntity.Random<ProductImage>()
                .WithProductId(product);

            var formFileData = new FormFileData("This is a test file", "File", productImage.Name);
            var formData = new Dictionary<string, string> {
                { "ProductId", product.ProductId.ToString() }
            };

            await PostAsync("/Product/AddImage", formData, formFileData, TestData.Admin);

            Mocks.FileWriterMock.Verify(m => m.ProcessAsync(It.Is<IFormFile>(f => f.FileName == productImage.Name), "productImages", formData["ProductId"]), Times.Never);
        }
    }
}
