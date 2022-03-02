namespace SupplementStore.Application.Services {

    public interface IWishRemover {
        void Remove(string userId, string productId);
    }
}
