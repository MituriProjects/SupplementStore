using SupplementStore.Application.Models;
using SupplementStore.ViewModels.Shared;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Admin {

    public class OrdersVM {

        public PageVM Page { get; set; } = new PageVM();

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public IDictionary<string, string> UserEmails { get; set; } = new Dictionary<string, string>();
    }
}
