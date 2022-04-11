using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IProductImageService {
        IEnumerable<ProductImageDetails> LoadMany(string productId);
        ProductImageCreatorResult Create(string productId, string imageName);
        ProductImageRemoverResult Remove(string productId, string name);
        void AppointMain(string productId, string name);
    }
}
