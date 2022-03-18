using SupplementStore.Application.Args;

namespace SupplementStore.Application.Services {

    public interface IOpinionCreator {
        void Create(OpinionCreatorArgs args);
    }
}
