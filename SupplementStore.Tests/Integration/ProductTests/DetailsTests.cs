using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class DetailsTests : IntegrationTestsBase {

        [TestMethod]
        public async Task IdIsValid_ReturnsProductDetails() {

            var products = TestEntity.Random<Product>(3);

            await GetAsync($"/Product/Details/{products[1].ProductId}");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price));
        }

        [TestMethod]
        public async Task NoProductWithThisId_RedirectsToIndex() {

            var products = TestEntity.Random<Product>(2);

            await GetAsync($"/Product/Details/{Guid.NewGuid().ToString()}");

            ExamineRedirect("/Product");
        }

        [TestMethod]
        public async Task IdIsInvalid_RedirectsToIndex() {

            var products = TestEntity.Random<Product>(2);

            await GetAsync($"/Product/Details/InvalidId");

            ExamineRedirect("/Product");
        }
    }
}
