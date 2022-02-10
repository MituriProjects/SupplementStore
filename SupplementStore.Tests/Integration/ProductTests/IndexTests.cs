using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Entities.Products;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsDetailsOfProducts() {

            var products = TestEntity.Random<Product>(2);

            await GetAsync("/Product");

            Examine(ContentScheme.Html()
                .Contains("ProductId", products[0].Id)
                .Contains("ProductName", products[0].Name)
                .Contains("ProductPrice", products[0].Price)
                .Contains("ProductId", products[1].Id)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price));
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

            await GetAsync("/Product?Skip=2&Take=2");

            var contentScheme = ContentScheme.Html();
            for (int i = 0; i < products.Count(); i++) {

                if (i < 2 || i == 4) {

                    contentScheme.Lacks("ProductId", products[i].Id);
                    contentScheme.Lacks("ProductName", products[i].Name);
                    contentScheme.Lacks("ProductPrice", products[i].Price);
                }
                else {

                    contentScheme.Contains("ProductId", products[i].Id);
                    contentScheme.Contains("ProductName", products[i].Name);
                    contentScheme.Contains("ProductPrice", products[i].Price);
                }
            }

            Examine(contentScheme);
        }
    }
}
