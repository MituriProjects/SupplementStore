using SupplementStore.Application.Models;
using SupplementStore.Infrastructure.ModelMapping;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.AppServices.Messages {

    public partial class MessageService {

        public IEnumerable<MessageDetails> LoadMany() {

            return MessageRepository.Entities
                .OrderByDescending(e => e.CreatedOn)
                .ToDetails(UserRepository);
        }
    }
}
