using SupplementStore.Application.Services;
using SupplementStore.Domain.Messages;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService : IMessageService {

        MessageFactory MessageFactory { get; }

        IDomainApprover DomainApprover { get; }

        public MessageService(
            MessageFactory messageFactory,
            IDomainApprover domainApprover) {

            MessageFactory = messageFactory;
            DomainApprover = domainApprover;
        }
    }
}
