using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IOrderService {
        OrderDetails Load(string id);
        OrderListResult LoadMany(OrderListArgs args);
        OrderDetails Create(OrderCreateArgs args);
    }
}
