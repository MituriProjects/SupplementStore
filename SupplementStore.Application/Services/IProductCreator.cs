using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IProductCreator {
        ProductDetails Create(string name, decimal price);
    }
}
