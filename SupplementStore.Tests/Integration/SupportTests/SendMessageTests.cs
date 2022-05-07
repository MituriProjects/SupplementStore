using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.SupportTests {

    [TestClass]
    public class SendMessageTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Post_DataIsValid_RedirectsToSendMessage() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", "newuser@test.pl" }
            };

            await PostAsync("/Support/SendMessage", formData);

            ExamineRedirect("/Support/SendMessage");
        }

        [TestMethod]
        public async Task Post_UserIsLoggedOut_CreatesMessage() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", TestData.User.Email }
            };

            await PostAsync("/Support/SendMessage", formData);

            TestDocument<Message>.Single(e => e.Text == formData["Text"] && e.SenderEmail == null && e.UserId == TestData.User.Id);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_NoUserWithThisEmail_CreatesMessage() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", "unknownuser@test.pl" }
            };

            await PostAsync("/Support/SendMessage", formData);

            TestDocument<Message>.Single(e => e.Text == formData["Text"] && e.SenderEmail == formData["Email"] && e.UserId == null);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_UserIsLoggedIn_CreatesMessage() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", TestData.User.Email }
            };

            await PostAsync("/Support/SendMessage", formData, TestData.User);

            TestDocument<Message>.Single(e => e.Text == formData["Text"] && e.SenderEmail == null && e.UserId == TestData.User.Id);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task Post_TextIsEmpty_NoMessageCreated() {

            var formData = new Dictionary<string, string> {
                { "Text", "" },
                { "Email", "unknownuser@test.pl" }
            };

            await PostAsync("/Support/SendMessage", formData);

            TestDocument<Message>.None(e => e.Text == formData["Text"] && e.SenderEmail == formData["Email"]);
            Examine(ContentScheme.Html()
                .Contains("Text", formData["Text"])
                .Contains("Email", formData["Email"]));
        }

        [TestMethod]
        public async Task Post_EmailIsEmpty_NoMessageCreated() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", "" }
            };

            await PostAsync("/Support/SendMessage", formData);

            TestDocument<Message>.None(e => e.Text == formData["Text"] && e.SenderEmail == formData["Email"]);
            Examine(ContentScheme.Html()
                .Contains("Text", formData["Text"])
                .Contains("Email", formData["Email"]));
        }

        [TestMethod]
        public async Task Post_EmailIsInvalid_NoMessageCreated() {

            var formData = new Dictionary<string, string> {
                { "Text", "TestText" },
                { "Email", "InvalidEmail" }
            };

            await PostAsync("/Support/SendMessage", formData);

            TestDocument<Message>.None(e => e.Text == formData["Text"] && e.SenderEmail == formData["Email"]);
            Examine(ContentScheme.Html()
                .Contains("Text", formData["Text"])
                .Contains("Email", formData["Email"]));
        }
    }
}
