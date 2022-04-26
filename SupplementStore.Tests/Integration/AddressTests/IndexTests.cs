using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AddressTests {

    [TestClass]
    public class IndexTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Address");

            ExamineAuthRedirect("/Address");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsAddressDetails() {

            var addresses = TestEntity.Random<Address>(2);
            addresses[0]
                .WithUserId(TestData.User);
            addresses[1]
                .WithUserId(TestData.User);

            await GetAsync("/Address", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", addresses[0].AddressId)
                .Contains("Street", addresses[0].Street)
                .Contains("PostalCode", addresses[0].PostalCode.Value)
                .Contains("City", addresses[0].City)
                .Contains("Id", addresses[1].AddressId)
                .Contains("Street", addresses[1].Street)
                .Contains("PostalCode", addresses[1].PostalCode.Value)
                .Contains("City", addresses[1].City));
        }
    }
}
