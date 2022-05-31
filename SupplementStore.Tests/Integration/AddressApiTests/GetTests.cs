using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AddressApiTests {

    [TestClass]
    public class GetTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_ReturnsEmptyCollection() {

            await GetAsync("api/address");

            Examine("[]");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_ReturnsUserAddresses() {

            var addresses = TestEntity.Random<Address>(4);
            addresses[1]
                .WithUserId(TestData.User);
            addresses[3]
                .WithUserId(TestData.User);

            await GetAsync("api/address", TestData.User);

            Examine(ContentScheme.Json()
                .Contains("Id", addresses[1].AddressId)
                .Contains("Street", addresses[1].Street)
                .Contains("PostalCode", addresses[1].PostalCode.Value)
                .Contains("City", addresses[1].City)
                .Contains("Id", addresses[3].AddressId)
                .Contains("Street", addresses[3].Street)
                .Contains("PostalCode", addresses[3].PostalCode.Value)
                .Contains("City", addresses[3].City));
        }
    }
}
