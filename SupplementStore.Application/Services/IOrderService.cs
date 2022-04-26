using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IOrderService {
        OrderDetails Load(string id);
        IEnumerable<OrderDetails> LoadMany();
        OrderDetails Create(OrderCreateArgs args);
    }
}
