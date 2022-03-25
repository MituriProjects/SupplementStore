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
            var orderProducts = TestEntity.Random<OrderProduct>(3);
            var opinions = TestEntity.Random<Opinion>(3);
            opinions[0]
                .WithGrade(new Grade(3))
                .WithOrderProductId(orderProducts[1]);
            opinions[1]
                .WithGrade(new Grade(5))
                .WithOrderProductId(orderProducts[2]);
            opinions[2]
                .WithGrade(new Grade(2))
                .WithOrderProductId(orderProducts[0]);
            orderProducts[0]
                .WithProductId(products[0])
                .WithOpinionId(opinions[2]);
            orderProducts[1]
                .WithProductId(products[1])
                .WithOpinionId(opinions[0]);
            orderProducts[2]
                .WithProductId(products[0])
                .WithOpinionId(opinions[1]);

            await GetAsync("/Product");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[0].ProductId)
                .Contains("ProductName", products[0].Name)
                .Contains("ProductPrice", products[0].Price)
                .Contains("AverageGrade", "3,5")
                .Contains("GradeCount", 2)
                .Contains("ProductId", products[1].ProductId)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price)
                .Contains("AverageGrade", "3")
                .Contains("GradeCount", 1));
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
            var orderProducts = TestEntity.Random<OrderProduct>(2);
            var opinions = TestEntity.Random<Opinion>(2);
            opinions[0]
                .WithGrade(new Grade(4))
                .WithOrderProductId(orderProducts[0]);
            opinions[1]
                .WithGrade(new Grade(1))
                .WithOrderProductId(orderProducts[1]);
            orderProducts[0]
                .WithProductId(products[3])
                .WithOpinionId(opinions[0]);
            orderProducts[1]
                .WithProductId(products[1])
                .WithOpinionId(opinions[1]);

            await GetAsync("/Product?Skip=2&Take=2");

            var contentScheme = ContentScheme.Html();
            for (int i = 0; i < products.Count(); i++) {

                if (i < 2 || i == 4) {

                    contentScheme.Lacks("ProductId", products[i].ProductId);
                    contentScheme.Lacks("ProductName", products[i].Name);
                    contentScheme.Lacks("ProductPrice", products[i].Price);
                    contentScheme.Lacks("AverageGrade", 1);
                }
                else {

                    contentScheme.Contains("ProductId", products[i].ProductId);
                    contentScheme.Contains("ProductName", products[i].Name);
                    contentScheme.Contains("ProductPrice", products[i].Price);

                    if (i == 2) {

                        contentScheme.Contains("AverageGrade", 0);
                        contentScheme.Contains("GradeCount", 0);
                    }

                    if (i == 3) {

                        contentScheme.Contains("AverageGrade", 4);
                        contentScheme.Contains("GradeCount", 1);
                    }
                }
            }

            Examine(contentScheme);
        }
    }
}
