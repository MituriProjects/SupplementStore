using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IProductOpinionsProvider {
        IEnumerable<ProductOpinionDetails> Load(string productId);
    }
}
