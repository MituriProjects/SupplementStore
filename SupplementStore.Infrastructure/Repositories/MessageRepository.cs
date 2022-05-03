using SupplementStore.Domain.Messages;

namespace SupplementStore.Infrastructure.Repositories {

    public class MessageRepository : Repository<Message>, IMessageRepository {

        public MessageRepository(IDocument<Message> document) : base(document) {
        }
    }
}
