namespace SupplementStore.Application.Services {

    public interface IBasketProductCreator {
        void Create(string userId, string productId, int quantity);
    }
}
