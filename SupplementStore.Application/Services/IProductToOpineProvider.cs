using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IProductToOpineProvider {
        ProductToOpineResult Load(string userId);
    }
}
