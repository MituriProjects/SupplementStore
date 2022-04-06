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
            var productImages = TestEntity.Random<ProductImage>(4);
            productImages[0]
                .WithProductId(products[1]);
            productImages[1]
                .WithProductId(products[2]);
            productImages[2]
                .WithProductId(products[0]);
            productImages[3]
                .WithProductId(products[1]);
            var purchases = TestEntity.Random<Purchase>(3);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithPurchaseId(purchases[0])
                .WithIsHidden(true);
            opinions[1]
                .WithPurchaseId(purchases[1]);
            purchases[0]
                .WithProductId(products[1])
                .WithOpinionId(opinions[0]);
            purchases[1]
                .WithProductId(products[1])
                .WithOpinionId(opinions[1]);

            await GetAsync($"/Product/Details/{products[1].ProductId}");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price)
                .Contains("ProductImage(0)", productImages[0].Name)
                .Contains("ProductImage(1)", productImages[3].Name)
                .Contains("OpinionStars", opinions[0].Rating.Stars)
                .Contains("OpinionText", opinions[0].Text)
                .Contains("OpinionIsHidden", opinions[0].IsHidden)
                .Contains("OpinionStars", opinions[1].Rating.Stars)
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
