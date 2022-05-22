using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Results {

    public class OrderListResult {

        public int AllOrdersCount { get; set; }

        public IEnumerable<OrderDetails> Orders { get; set; }
    }
}
