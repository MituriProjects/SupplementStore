using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IWishesProvider {
        IEnumerable<ProductDetails> Load(string userId);
    }
}
