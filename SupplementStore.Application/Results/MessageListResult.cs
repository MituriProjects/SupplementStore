using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Results {

    public class MessageListResult {

        public int AllMessagesCount { get; set; }

        public IEnumerable<MessageDetails> Messages { get; set; }
    }
}
