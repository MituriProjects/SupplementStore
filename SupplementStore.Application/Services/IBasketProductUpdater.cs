using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IBasketProductUpdater {
        void Update(BasketProductDetails basketProductDetails);
    }
}
