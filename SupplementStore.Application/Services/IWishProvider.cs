namespace SupplementStore.Application.Services {

    public interface IWishProvider {
        bool Load(string userId, string productId);
    }
}
