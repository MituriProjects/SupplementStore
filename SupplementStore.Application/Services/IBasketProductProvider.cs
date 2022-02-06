using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IBasketProductProvider {
        BasketProductDetails Load(string id);
    }
}
