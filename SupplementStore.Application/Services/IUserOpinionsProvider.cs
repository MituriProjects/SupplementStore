using SupplementStore.Application.Models;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IUserOpinionsProvider {
        IEnumerable<OpinionDetails> Load(string userId);
    }
}
