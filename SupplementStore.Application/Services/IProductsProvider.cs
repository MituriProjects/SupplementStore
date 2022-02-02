using SupplementStore.Application.Args;
using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IProductsProvider {
        ProductsProviderResult Load(ProductProviderArgs args);
    }
}
