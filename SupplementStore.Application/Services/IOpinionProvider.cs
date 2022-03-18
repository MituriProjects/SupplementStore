using SupplementStore.Application.Models;

namespace SupplementStore.Application.Services {

    public interface IOpinionProvider {
        OpinionDetails Load(string opinionId);
    }
}
