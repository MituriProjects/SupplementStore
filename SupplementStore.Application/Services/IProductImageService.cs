using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IProductImageService {
        IEnumerable<ProductImageDetails> LoadMany(string productId);
        ProductImageCreateResult Create(string productId, string imageName);
        ProductImageRemoveResult Remove(string productId, string name);
        void AppointMain(string productId, string name);
    }
}
