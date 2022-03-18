using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IOpinionProductProvider {
        ProductDetails Load(string opinionId);
    }
}
