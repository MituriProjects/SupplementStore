using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IOrderProvider {
        OrderDetails Load(string id);
    }
}
