using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IWishService {
        IEnumerable<ProductDetails> LoadMany(string userId);
        bool IsOnWishList(string userId, string productId);
        void Create(string userId, string productId);
        void Remove(string userId, string productId);
    }
}
