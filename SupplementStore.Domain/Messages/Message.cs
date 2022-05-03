using System;

namespace SupplementStore.Domain.Messages {

    public class Message : Entity {

        public MessageId MessageId { get; private set; } = new MessageId(Guid.Empty);

        public string SenderEmail { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }
    }
}
