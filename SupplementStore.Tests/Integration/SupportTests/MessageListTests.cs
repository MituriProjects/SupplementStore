using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.SupportTests {

    [TestClass]
    public class MessageListTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Support/MessageList");

            ExamineAuthRedirect("/Support/MessageList");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            await GetAsync("/Support/MessageList", TestData.User);

            ExamineAccessDeniedRedirect("/Support/MessageList");
        }

        [TestMethod]
        public async Task UserIsAuthorized_ReturnsMessageDetails() {

            var messages = TestEntity.Random<Message>(3);
            messages[0]
                .WithSenderEmail(null)
                .WithUserId(TestData.Users[0]);
            messages[2]
                .WithSenderEmail(null)
                .WithUserId(TestData.Users[1]);

            await GetAsync("/Support/MessageList", TestData.Admin);

            var content = ContentScheme.Html();
            foreach (var message in messages) {

                content.Contains("Email", message.SenderEmail
                    ?? TestData.Users.First(e => e.Id == message.UserId).Email);
                content.Contains("Text", message.Text);
                content.Contains("CreatedOn", message.CreatedOn);
            }
            Examine(content);
        }
    }
}
