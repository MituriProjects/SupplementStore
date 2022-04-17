using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AddressTests {

    [TestClass]
    public class EditTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            var addressId = Guid.NewGuid();

            await GetAsync($"/Address/Edit/{addressId}");

            ExamineAuthRedirect($"/Address/Edit/{addressId}");
        }

        [TestMethod]
        public async Task Get_AddressExists_ReturnsAddressDetails() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            await GetAsync($"/Address/Edit/{address.AddressId}", TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", address.AddressId)
                .Contains("Street", address.Street)
                .Contains("PostalCode", address.PostalCode.Value)
                .Contains("City", address.City));
        }

        [TestMethod]
        public async Task Get_AddressDoesNotExist_RedirectsToIndex() {

            await GetAsync($"/Address/Edit/{Guid.NewGuid()}", TestData.User);

            ExamineRedirect("/Address");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_RedirectsToLogin() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Street", "ul. Lubelska 12" },
                { "PostalCode", "33-822" },
                { "City", "Siedlce" }
            };

            await PostAsync("/Address/Edit", formData);

            ExamineAuthRedirect("/Address/Edit");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_RedirectsToIndex() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "ul. Narutowicza 43/23" },
                { "PostalCode", "18-534" },
                { "City", "Warszawa" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            ExamineRedirect("/Address");
        }

        [TestMethod]
        public async Task Post_FromDataIsValid_EditsAddress() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "ul. Radomska 49" },
                { "PostalCode", "15-251" },
                { "City", "Wolsztyn" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            TestDocument<Address>.Single(e =>
                e.AddressId == address.AddressId
                && e.UserId == TestData.User.Id
                && e.Street == formData["Street"]
                && e.PostalCode.Value == formData["PostalCode"]
                && e.City == formData["City"]
                && e.IsHidden == false);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_AddressDoesNotExist_NoChangesSavedAndExceptionThrown() {

            var formData = new Dictionary<string, string> {
                { "Id", Guid.NewGuid().ToString() },
                { "Street", "ul. Kurantowa 13/9" },
                { "PostalCode", "45-150" },
                { "City", "Trzcianka" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            ExamineNoExceptionThrown();
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_AddressDoesNotBelongToUser_NoAddressEdition() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.Users[1].Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "ul. Misjonarska 11/5" },
                { "PostalCode", "08-412" },
                { "City", "Kostrzyn" }
            };

            await PostAsync("/Address/Edit", formData, TestData.Users[0]);

            TestDocument<Address>.Single(e =>
                e.AddressId == address.AddressId
                && e.UserId == TestData.Users[1].Id
                && e.Street != formData["Street"]
                && e.PostalCode.Value != formData["PostalCode"]
                && e.City != formData["City"]
                && e.IsHidden == false);
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task Post_StreetIsEmpty_ReturnsDetails() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "" },
                { "PostalCode", "12-723" },
                { "City", "Rypin" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", formData["Id"])
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_PostalCodeIsEmpty_ReturnsDetails() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "Laurowa 15" },
                { "PostalCode", "" },
                { "City", "Zuromin" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", formData["Id"])
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_PostalCodeIsInvalid_ReturnsDetails() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "Narcyzowa 53" },
                { "PostalCode", "144-31" },
                { "City", "Czersk" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", formData["Id"])
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }

        [TestMethod]
        public async Task Post_CityIsEmpty_ReturnsDetails() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            var formData = new Dictionary<string, string> {
                { "Id", address.AddressId.ToString() },
                { "Street", "Maszynowa 32/29" },
                { "PostalCode", "32-672" },
                { "City", "" }
            };

            await PostAsync("/Address/Edit", formData, TestData.User);

            Examine(ContentScheme.Html()
                .Contains("Id", formData["Id"])
                .Contains("Street", formData["Street"])
                .Contains("PostalCode", formData["PostalCode"])
                .Contains("City", formData["City"]));
        }
    }
}
