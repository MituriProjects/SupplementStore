using SupplementStore.Application.Args;
using SupplementStore.Application.Results;
using SupplementStore.Domain.Messages;
using SupplementStore.Infrastructure.ArgsMapping;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService {

        public MessageCreateResult Create(MessageCreateArgs args) {

            var message = MessageFactory.Create(args.ToFactoryArgs());

            if (message == null)
                return MessageCreateResult.Failed;

            DomainApprover.SaveChanges();

            return MessageCreateResult.Succeeded;
        }
    }
}
