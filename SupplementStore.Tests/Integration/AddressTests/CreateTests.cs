using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AddressTests {

    [TestClass]
    public class CreateTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Address/Create");

            ExamineAuthRedirect("/Address/Create");
        }

        [TestMethod]
        public async Task Get_UserIsLoggedIn_ReturnsViewDetails() {

            await GetAsync("/Address/Create", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Street", "")
                .Contains("PostalCode", "")
                .Contains("City", ""));
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Daniewskiego 14/77" },
                { "PostalCode", "24-030" },
                { "City", "Poznań" }
            };

            await PostAsync("/Address/Create", formData);

            ExamineAuthRedirect("/Address/Create");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_RedirectsToIndex() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Radosna 4/13" },
                { "PostalCode", "12-556" },
                { "City", "Olsztyn" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            ExamineRedirect("/Address");
        }

        [TestMethod]
        public async Task Post_FromDataIsValid_CreatesAddress() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Leśna 12" },
                { "PostalCode", "22-339" },
                { "City", "Radzymin" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.UserId == TestData.User.Id
                && e.Street == formData["Street"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.IsHidden == false);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_StreetIsEmpty_RedirectsToIndex() {

            var formData = new Dictionary<string, string> {
                { "Street", "" },
                { "PostalCode", "06-686" },
                { "City", "Niemce" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_PostalCodeIsInvalid_RedirectsToIndex() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Spokojna 22/21" },
                { "PostalCode", "180-56" },
                { "City", "Szczecin" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_PostalCodeIsEmpty_RedirectsToIndex() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Zielona 8/46" },
                { "PostalCode", "" },
                { "City", "Bychawa" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_CityIsEmpty_RedirectsToIndex() {

            var formData = new Dictionary<string, string> {
                { "Street", "ul. Romera 47" },
                { "PostalCode", "20-485" },
                { "City", "" }
            };

            await PostAsync("/Address/Create", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }
    }
}
