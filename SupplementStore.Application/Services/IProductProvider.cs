using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IProductProvider {
        ProductDetails Load(string productId);
    }
}
