using SupplementStore.Application.Args;
using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IMessageService {
        MessageCreateResult Create(MessageCreateArgs args);
    }
}
