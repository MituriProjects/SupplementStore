using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.WishApiTests {

    [TestClass]
    public class GetTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_ReturnsFalse() {

            var product = TestEntity.Random<Product>();

            await GetAsync($"api/wish/{product.ProductId}");

            Examine("false");
        }

        [TestMethod]
        public async Task InvalidProductId_ReturnsFalse() {

            await GetAsync("api/wish/InvalidProductId", TestData.User);

            Examine("false");
        }

        [TestMethod]
        public async Task ProductDoesNotExist_ReturnsFalse() {

            await GetAsync($"api/wish/{Guid.NewGuid()}", TestData.User);

            Examine("false");
        }

        [TestMethod]
        public async Task ProductIsOnWishList_ReturnsTrue() {

            var product = TestEntity.Random<Product>();
            var wish = TestEntity.Random<Wish>();
            wish
                .WithProductId(product)
                .WithUserId(TestData.User);

            await GetAsync($"api/wish/{product.ProductId}", TestData.User);

            Examine("true");
        }

        [TestMethod]
        public async Task ProductIsNotOnWishList_ReturnsFalse() {

            var product = TestEntity.Random<Product>();

            await GetAsync($"api/wish/{product.ProductId}", TestData.User);

            Examine("false");
        }
    }
}
