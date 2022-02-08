using SupplementStore.Application.Args;
using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IOrderCreator {
        OrderDetails Create(OrderCreatorArgs args);
    }
}
