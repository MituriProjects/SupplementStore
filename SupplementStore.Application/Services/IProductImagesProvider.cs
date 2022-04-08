using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IProductImagesProvider {
        IEnumerable<ProductImageDetails> Load(string productId);
    }
}
