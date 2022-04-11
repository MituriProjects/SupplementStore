using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IProductService {
        ProductDetails Load(string productId);
        ProductsProviderResult LoadMany(ProductsProviderArgs args);
        IEnumerable<ProductOpinionDetails> LoadOpinions(string productId);
        ProductDetails Create(string name, decimal price);
        void Update(ProductDetails productDetails);
    }
}
