using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class DetailsTests : IntegrationTestsBase {

        [TestMethod]
        public async Task IdIsValid_ReturnsProductDetails() {

            var products = TestEntity.Random<Product>(3);
            var orderProducts = TestEntity.Random<OrderProduct>(3);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithOrderProductId(orderProducts[0].OrderProductId)
                .WithIsHidden(true);
            opinions[1]
                .WithOrderProductId(orderProducts[1].OrderProductId);
            orderProducts[0]
                .WithProductId(products[1].ProductId)
                .WithOpinionId(opinions[0].OpinionId);
            orderProducts[1]
                .WithProductId(products[1].ProductId)
                .WithOpinionId(opinions[1].OpinionId);

            await GetAsync($"/Product/Details/{products[1].ProductId}");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price)
                .Contains("OpinionStars", opinions[0].Grade.Stars)
                .Contains("OpinionText", opinions[0].Text)
                .Contains("OpinionIsHidden", opinions[0].IsHidden)
                .Contains("OpinionStars", opinions[1].Grade.Stars)
                .Contains("OpinionText", opinions[1].Text)
                .Contains("OpinionIsHidden", opinions[1].IsHidden));
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
