using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.BasketTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsBasketProductDetails() {

            var products = TestEntity.Random<Product>(4);
            var basketProducts = TestEntity.Random<BasketProduct>(4);
            for (int i = 0; i < basketProducts.Length; i++)
                basketProducts[i].WithProductId(products[i].ProductId);
            basketProducts[1].WithUserId(TestData.User.Id);
            basketProducts[3].WithUserId(TestData.User.Id);

            await GetAsync("/Basket", TestData.User);

            var contentScheme = ContentScheme.Html();
            foreach (var basketProduct in basketProducts) {

                var product = products.First(e => e.ProductId == basketProduct.ProductId);

                var values = new Dictionary<string, object> {
                    { "Id", basketProduct.BasketProductId },
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
