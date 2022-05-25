using SupplementStore.Application.Args;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Messages;
using SupplementStore.Domain.Shared;
using SupplementStore.Infrastructure.ModelMapping;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService {

        public MessageListResult LoadMany(MessageListArgs args) {

            var messages = MessageRepository
                .FindBy(new PagingFilter<Message>(args.Skip, args.Take, e => e.CreatedOn))
                .ToDetails(UserRepository);

            return new MessageListResult {
                AllMessagesCount = MessageRepository.Count(),
                Messages = messages
            };
        }
    }
}
