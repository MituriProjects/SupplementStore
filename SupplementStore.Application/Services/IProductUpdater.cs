using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IProductUpdater {
        void Update(ProductDetails productDetails);
    }
}
