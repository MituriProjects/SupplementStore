using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IProductImageRemover {
        ProductImageRemoverResult Remove(string productId, string name);
    }
}
