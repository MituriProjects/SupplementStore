using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.WishApiTests {

    [TestClass]
    public class PostTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_NoWishCreated() {

            var product = TestEntity.Random<Product>();

            await PostAsync($"api/wish/{product.ProductId}");

            TestDocument<Wish>.None(e => e.ProductId == product.ProductId);
        }

        [TestMethod]
        public async Task ValidProductId_CreatesWish() {

            var product = TestEntity.Random<Product>();

            await PostAsync($"api/wish/{product.ProductId}", TestData.User);

            TestDocument<Wish>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.ProductId);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task InvalidProductId_NoWishCreated() {

            await PostAsync("api/wish/InvalidProductId", TestData.User);

            TestDocument<Wish>.None(e => e.UserId == TestData.User.Id);
        }

        [TestMethod]
        public async Task ProductDoesNotExist_NoWishCreated() {

            await PostAsync($"api/wish/{Guid.NewGuid()}", TestData.User);

            TestDocument<Wish>.None(e => e.UserId == TestData.User.Id);
        }

        [TestMethod]
        public async Task ProductIsOnWishList_NoWishCreated() {

            var product = TestEntity.Random<Product>();
            var wish = TestEntity.Random<Wish>();
            wish
                .WithProductId(product)
                .WithUserId(TestData.User);

            await PostAsync($"api/wish/{product.ProductId}", TestData.User);

            TestDocument<Wish>.Single(e => e.UserId == TestData.User.Id && e.ProductId == product.ProductId);
        }
    }
}
