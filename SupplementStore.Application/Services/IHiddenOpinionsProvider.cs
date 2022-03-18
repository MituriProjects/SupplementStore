using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IHiddenOpinionsProvider {
        IEnumerable<HiddenOpinionDetails> Load();
    }
}
