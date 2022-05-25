using SupplementStore.Application.Models;
using SupplementStore.ViewModels.Shared;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Support {

    public class MessageListVM {

        public PageVM Page { get; set; } = new PageVM();

        public IEnumerable<MessageDetails> Messages { get; set; }
    }
}
