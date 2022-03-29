using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsDetailsOfProducts() {

            var products = TestEntity.Random<Product>(2);
            var purchases = TestEntity.Random<Purchase>(3);
            var opinions = TestEntity.Random<Opinion>(3);
            opinions[0]
                .WithRating(new Rating(3))
                .WithPurchaseId(purchases[1]);
            opinions[1]
                .WithRating(new Rating(5))
                .WithPurchaseId(purchases[2]);
            opinions[2]
                .WithRating(new Rating(2))
                .WithPurchaseId(purchases[0]);
            purchases[0]
                .WithProductId(products[0])
                .WithOpinionId(opinions[2]);
            purchases[1]
                .WithProductId(products[1])
                .WithOpinionId(opinions[0]);
            purchases[2]
                .WithProductId(products[0])
                .WithOpinionId(opinions[1]);

            await GetAsync("/Product");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[0].ProductId)
                .Contains("ProductName", products[0].Name)
                .Contains("ProductPrice", products[0].Price)
                .Contains("AverageRating", "3,5")
                .Contains("RatingCount", 2)
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price)
                .Contains("AverageRating", "3")
                .Contains("RatingCount", 1));
        }

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsAllProductsCount() {

            var products = TestEntity.Random<Product>(3);

            await GetAsync("/Product");

            Examine(ContentScheme.Html()
                .Contains("AllProductsCount", 3));
        }

        [TestMethod]
        public async Task SkipEquals2AndTakeEquals2_ReturnsDetailsOfProducts() {

            var products = TestEntity.Random<Product>(5);
            var purchases = TestEntity.Random<Purchase>(2);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithRating(new Rating(4))
                .WithPurchaseId(purchases[0]);
            opinions[1]
                .WithRating(new Rating(1))
                .WithPurchaseId(purchases[1]);
            purchases[0]
                .WithProductId(products[3])
                .WithOpinionId(opinions[0]);
            purchases[1]
                .WithProductId(products[1])
                .WithOpinionId(opinions[1]);

            await GetAsync("/Product?Skip=2&Take=2");

            var contentScheme = ContentScheme.Html();
            for (int i = 0; i < products.Count(); i++) {

                if (i < 2 || i == 4) {

                    contentScheme.Lacks("ProductId", products[i].ProductId);
                    contentScheme.Lacks("ProductName", products[i].Name);
                    contentScheme.Lacks("ProductPrice", products[i].Price);
                    contentScheme.Lacks("AverageRating", 1);
                }
                else {

                    contentScheme.Contains("ProductId", products[i].ProductId);
                    contentScheme.Contains("ProductName", products[i].Name);
                    contentScheme.Contains("ProductPrice", products[i].Price);

                    if (i == 2) {

                        contentScheme.Contains("AverageRating", 0);
                        contentScheme.Contains("RatingCount", 0);
                    }

                    if (i == 3) {

                        contentScheme.Contains("AverageRating", 4);
                        contentScheme.Contains("RatingCount", 1);
                    }
                }
            }

            Examine(contentScheme);
        }
    }
}
