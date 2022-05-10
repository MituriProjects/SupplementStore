using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IMessageService {
        IEnumerable<MessageDetails> LoadMany();
        MessageCreateResult Create(MessageCreateArgs args);
    }
}
