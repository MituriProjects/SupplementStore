using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IOpinionsProvider {
        IEnumerable<OpinionDetails> Load(string userId);
    }
}
