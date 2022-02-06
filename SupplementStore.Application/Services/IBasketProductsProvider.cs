using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IBasketProductsProvider {
        IEnumerable<BasketProductDetails> Load(string userId);
    }
}
