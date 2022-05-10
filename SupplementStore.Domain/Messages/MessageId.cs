using System;

namespace SupplementStore.Domain.Messages {

    public class MessageId : IdBase {

        public MessageId(Guid id) : base(id) {
        }

        public MessageId(string id) : base(id) {
        }
    }
}
