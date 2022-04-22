using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Addresses;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AddressTests {

    [TestClass]
    public class DeleteTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var address = TestEntity.Random<Address>();

            await PostAsync($"/Address/Delete/{address.AddressId}");

            ExamineAuthRedirect($"/Address/Delete/{address.AddressId}");
        }

        [TestMethod]
        public async Task UserIsLoggedIn_RedirectsToIndex() {

            var address = TestEntity.Random<Address>();

            await PostAsync($"/Address/Delete/{address.AddressId}", TestData.User);

            ExamineRedirect("/Address");
        }

        [TestMethod]
        public async Task AddressExistsAndBelongsToUser_HidesAddress() {

            var address = TestEntity.Random<Address>()
                .WithUserId(TestData.User.Id);

            await PostAsync($"/Address/Delete/{address.AddressId}", TestData.User);

            TestDocument<Address>.Single(e => e.AddressId == address.AddressId && e.IsHidden);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task AddressDoesNotExist_NoChangesSavedAndExceptionThrown() {

            await PostAsync($"/Address/Delete/{Guid.NewGuid()}", TestData.User);

            ExamineNoExceptionThrown();
            TestDocumentApprover.ExamineNoChangesSaved();
        }

        [TestMethod]
        public async Task AddressDoesNotBelongToUser_NoAddressHiding() {

            var address = TestEntity.Random<Address>();

            await PostAsync($"/Address/Delete/{address.AddressId}", TestData.User);

            TestDocument<Address>.Single(e => e.AddressId == address.AddressId && e.IsHidden == false);
            TestDocumentApprover.ExamineNoChangesSaved();
        }
    }
}
