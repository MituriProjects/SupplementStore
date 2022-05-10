using SupplementStore.Application.Services;
using SupplementStore.Domain.Messages;
using SupplementStore.Infrastructure.Repositories;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService : IMessageService {

        UserRepository UserRepository { get; }

        IMessageRepository MessageRepository { get; }

        MessageFactory MessageFactory { get; }

        IDomainApprover DomainApprover { get; }

        public MessageService(
            UserRepository userRepository,
            IMessageRepository messageRepository,
            MessageFactory messageFactory,
            IDomainApprover domainApprover) {

            UserRepository = userRepository;
            MessageRepository = messageRepository;
            MessageFactory = messageFactory;
            DomainApprover = domainApprover;
        }
    }
}
