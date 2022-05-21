using SupplementStore.Application.Args;
using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IMessageService {
        MessageListResult LoadMany(MessageListArgs args);
        MessageCreateResult Create(MessageCreateArgs args);
    }
}
