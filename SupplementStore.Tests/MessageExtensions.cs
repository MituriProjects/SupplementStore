using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Messages;

namespace SupplementStore.Tests {

    static class MessageExtensions {

        public static Message WithUserId(this Message message, IdentityUser user) {

            message.UserId = user.Id;

            return message;
        }

        public static Message WithSenderEmail(this Message message, string email) {

            message.SenderEmail = email;

            return message;
        }

        public static Message WithText(this Message message, string text) {

            message.Text = text;

            return message;
        }
    }
}
