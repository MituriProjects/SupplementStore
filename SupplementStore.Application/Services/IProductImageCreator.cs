using SupplementStore.Application.Results;

namespace SupplementStore.Application.Services {

    public interface IProductImageCreator {
        ProductImageCreatorResult Create(string productId, string imageName);
    }
}
