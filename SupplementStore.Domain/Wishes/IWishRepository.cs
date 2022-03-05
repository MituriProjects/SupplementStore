using SupplementStore.Domain.Products;

namespace SupplementStore.Domain.Wishes {

    public interface IWishRepository : IRepository<Wish> {
        void Delete(string userId, ProductId productId);
    }
}
