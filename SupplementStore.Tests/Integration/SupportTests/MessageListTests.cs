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

        [TestMethod]
        public async Task DefaultSkipAndTake_ReturnsAllMessagesCount() {

            var messages = TestEntity.Random<Message>(3);

            await GetAsync("/Support/MessageList", TestData.Admin);

            Examine(ContentScheme.Html()
                .Contains("AllMessagesCount", 3));
        }

        [TestMethod]
        public async Task SkipEquals2AndTakeEquals2_ReturnsDetailsOfAppropriateMessages() {

            var messages = TestEntity.Random<Message>(5);

            await GetAsync("/Support/MessageList?Page.Skip=2&Page.Take=2", TestData.Admin);

            messages = messages
                .OrderByDescending(e => e.CreatedOn)
                .ToArray();

            var contentScheme = ContentScheme.Html();
            for (int i = 0; i < messages.Count(); i++) {

                if (i < 2 || i > 3) {

                    contentScheme.Lacks("Email", messages[i].SenderEmail);
                    contentScheme.Lacks("Text", messages[i].Text);
                    contentScheme.Lacks("CreatedOn", messages[i].CreatedOn);
                }
                else {

                    contentScheme.Contains("Email", messages[i].SenderEmail);
                    contentScheme.Contains("Text", messages[i].Text);
                    contentScheme.Contains("CreatedOn", messages[i].CreatedOn);
                }
            }

            Examine(contentScheme);
        }
    }
}
