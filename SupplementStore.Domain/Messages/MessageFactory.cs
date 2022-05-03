namespace SupplementStore.Domain.Messages {

    public class MessageFactory {

        IMessageRepository MessageRepository { get; }

        public MessageFactory(IMessageRepository messageRepository) {

            MessageRepository = messageRepository;
        }

        public Message Create(MessageFactoryArgs args) {

            var message = new Message {
                Text = args.Text,
                UserId = args.UserId,
                SenderEmail = args.UserId == null ? args.SenderEmail : null
            };

            MessageRepository.Add(message);

            return message;
        }
    }
}
