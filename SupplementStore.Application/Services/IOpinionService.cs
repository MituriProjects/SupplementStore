using SupplementStore.Application.Args;
using SupplementStore.Application.Models;
using SupplementStore.Application.Results;
using System.Collections.Generic;

namespace SupplementStore.Application.Services {

    public interface IOpinionService {
        OpinionDetails Load(string opinionId);
        IEnumerable<OpinionDetails> LoadMany(string userId);
        HiddenOpinionListResult LoadHidden(HiddenOpinionListArgs args);
        ProductDetails LoadOpinionProduct(string opinionId);
        ProductToOpineResult LoadProductToOpine(string userId);
        void Create(OpinionCreateArgs args);
        void UpdateText(string opinionId, string text);
        void Hide(string opinionId);
        void Reveal(string opinionId);
    }
}
