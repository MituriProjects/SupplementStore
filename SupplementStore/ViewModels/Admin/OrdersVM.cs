using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.ViewModels.Admin {

    public class OrdersVM {

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public IDictionary<string, string> UserEmails { get; set; }
    }
}
