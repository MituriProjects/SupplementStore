using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IBasketProductService {
        BasketProductDetails Load(string Id);
        IEnumerable<BasketProductDetails> LoadMany(string userId);
        void Create(string userId, string productId, int quantity);
        void Update(BasketProductDetails basketProductDetails);
        void Remove(string userId, string productId);
    }
}
