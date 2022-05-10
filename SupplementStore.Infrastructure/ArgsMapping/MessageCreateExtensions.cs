using SupplementStore.Application.Args;
using SupplementStore.Domain.Messages;

namespace SupplementStore.Infrastructure.ArgsMapping {

    static class MessageCreateExtensions {

        public static MessageFactoryArgs ToFactoryArgs(this MessageCreateArgs args) {

            return new MessageFactoryArgs {
                Text = args.Text,
                UserId = args.UserId,
                SenderEmail = args.SenderEmail
            };
        }
    }
}
