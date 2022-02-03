using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsBasketProductDetails() {

            var products = TestProduct.Random(4);
            var basketProducts = TestBasketProduct.Random(4);
            for (int i = 0; i < basketProducts.Length; i++)
                basketProducts[i].WithProductId(products[i].Id);
            basketProducts[1].WithUserId(TestData.User.Id);
            basketProducts[3].WithUserId(TestData.User.Id);

            await GetAsync("/Basket", TestData.User);

            var contentScheme = new ContentScheme();
            foreach (var basketProduct in basketProducts) {

                var product = products.First(e => e.Id == basketProduct.ProductId);

                var values = new Dictionary<string, object> {
                    { "ProductId", basketProduct.ProductId },
                    { "ProductName", product.Name },
                    { "ProductPrice", product.Price },
                    { "Quantity", basketProduct.Quantity }
                };

                foreach (var value in values) {

                    if (basketProduct.UserId == TestData.User.Id) {

                        contentScheme.Contains(value.Key, value.Value);
                    }
                    else {

                        contentScheme.Lacks(value.Key, value.Value);
                    }
                }
            }

            Examine(contentScheme);
        }

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Basket");

            ExamineAuthRedirect("/Basket");
        }
    }
}
