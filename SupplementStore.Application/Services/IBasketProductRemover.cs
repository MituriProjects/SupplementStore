namespace SupplementStore.Application.Services {

    public interface IBasketProductRemover {
        void Remove(string userId, string productId);
    }
}
