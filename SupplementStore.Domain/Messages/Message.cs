using System;

namespace SupplementStore.Domain.Messages {

    public class Message : UserEntity {

        public MessageId MessageId { get; private set; } = new MessageId(Guid.Empty);

        public string SenderEmail { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
