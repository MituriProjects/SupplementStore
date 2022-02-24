using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class EditTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            var id = Guid.NewGuid();

            await GetAsync($"/Product/Edit/{id}");

            ExamineAuthRedirect($"/Product/Edit/{id}");
        }

        [TestMethod]
        public async Task Get_UserIsNotAuthorized_AccessDenied() {

            var id = Guid.NewGuid();

            await GetAsync($"/Product/Edit/{id}", TestData.User);

            ExamineAccessDeniedRedirect($"/Product/Edit/{id}");
        }

        [TestMethod]
        public async Task Get_ProductExists_ReturnsProductDetails() {

            var products = TestEntity.Random<Product>(2);

            await GetAsync($"/Product/Edit/{products[1].ProductId}", TestData.Admin);

            Examine(ContentScheme.Html()
                .Lacks("Id", products[0].ProductId)
                .Lacks("Name", products[0].Name)
                .Lacks("Price", products[0].Price)
                .Contains("Id", products[1].ProductId)
                .Contains("Name", products[1].Name)
                .Contains("Price", products[1].Price));
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Name", "TestProductName34" },
                { "Price", 12.45M.ToString() }
            };

            await PostAsync("/Product/Edit", formData);

            ExamineAuthRedirect("/Product/Edit");
        }

        [TestMethod]
        public async Task Post_UserIsNotAuthorized_AccessDenied() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Name", "TestProductName8" },
                { "Price", 31.53M.ToString() }
            };

            await PostAsync("/Product/Edit", formData, TestData.User);

            ExamineAccessDeniedRedirect("/Product/Edit");
        }

        [TestMethod]
        public async Task Post_UserIsAdmin_RedirectsToDetails() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Name", "TestProductName8" },
                { "Price", 31.53M.ToString() }
            };

            await PostAsync("/Product/Edit", formData, TestData.Admin);

            ExamineRedirect($"/Product/Details/{formData["Id"]}");
        }

        [TestMethod]
        public async Task Post_ProductExists_UpdatesProduct() {

            var product = TestEntity.Random<Product>();

            var formData = new Dictionary<string, string> {
                { "Id", product.ProductId.ToString() },
                { "Name", $"NewProductName-{product.ProductId}" },
                { "Price", (product.Price + 18.76M).ToString() }
            };

            await PostAsync("/Product/Edit", formData, TestData.Admin);

            Assert.AreEqual(formData["Name"], product.Name);
            Assert.AreEqual(formData["Price"], product.Price.ToString());
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_NewPriceIsInvalid_ReturnsProductDetails() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Name", "TestProductName32" },
                { "Price", "InvalidPrice" }
            };

            await PostAsync("/Product/Edit", formData, TestData.Admin);

            Examine(ContentScheme.Html()
                .Contains("Id", formData["Id"])
                .Contains("Name", formData["Name"])
                .Contains("Price", "0"));
        }

        [TestMethod]
        public async Task Post_ProductIdIsEmpty_CreatesNewProduct() {

            var productName = "TestProductName8";
            var productPrice = 9.45M;

            var formData = new Dictionary<string, string> {
                { "Id", "" },
                { "Name", productName },
                { "Price", productPrice.ToString() }
            };

            await PostAsync("/Product/Edit", formData, TestData.Admin);

            TestDocument<Product>.Single(e => e.Name == productName && e.Price == productPrice);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_ProductIdIsEmpty_RedirectToDetails() {

            var productName = "TestProductName13";
            var productPrice = 13.14M;

            var formData = new Dictionary<string, string> {
                { "Id", "" },
                { "Name", productName },
                { "Price", productPrice.ToString() }
            };

            await PostAsync("/Product/Edit", formData, TestData.Admin);

            var createdProduct = TestDocument<Product>.First(e => e.Name == productName && e.Price == productPrice);

            ExamineRedirect($"/Product/Details/{createdProduct.ProductId}");
        }
    }
}
