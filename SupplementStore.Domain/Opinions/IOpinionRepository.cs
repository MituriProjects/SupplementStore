using System.Collections.Generic;

namespace SupplementStore.Domain.Opinions {

    public interface IOpinionRepository : IRepository<Opinion> {
        Opinion FindBy(OpinionId opinionId);
        IEnumerable<Opinion> FindBy(IEnumerable<OpinionId> opinionIds);
    }
}
