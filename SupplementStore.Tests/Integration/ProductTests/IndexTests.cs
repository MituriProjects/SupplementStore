using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsDetailsOfProducts() {

            var products = TestProduct.Random(2);

            await GetAsync("/Product");

            Examine(new ContentScheme()
                .Contains("ProductId", products[0].Id)
                .Contains("ProductName", products[0].Name)
                .Contains("ProductPrice", products[0].Price)
                .Contains("ProductId", products[1].Id)
                .Contains("ProductName", products[1].Name)
                .Contains("ProductPrice", products[1].Price));
        }

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsAllProductsCount() {

            var products = TestProduct.Random(3);

            await GetAsync("/Product");

            Examine(new ContentScheme()
                .Contains("AllProductsCount", 3));
        }

        [TestMethod]
        public async Task SkipEquals2AndTakeEquals2_ReturnsDetailsOfProducts() {

            var products = TestProduct.Random(5);

            await GetAsync("/Product?Skip=2&Take=2");

            var contentScheme = new ContentScheme();
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
