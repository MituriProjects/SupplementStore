using SupplementStore.Application.Args;
using SupplementStore.Application.Results;
using SupplementStore.Infrastructure.ModelMapping;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService {

        public MessageListResult LoadMany(MessageListArgs args) {

            var messages = MessageRepository.Entities
                .OrderByDescending(e => e.CreatedOn)
                .Skip(args.Skip)
                .Take(args.Take)
                .ToDetails(UserRepository);

            return new MessageListResult {
                AllMessagesCount = MessageRepository.Count(),
                Messages = messages
            };
        }
    }
}
