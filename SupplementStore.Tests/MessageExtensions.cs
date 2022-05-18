using SupplementStore.Domain.Messages;

namespace SupplementStore.Tests {

    static class MessageExtensions {

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
