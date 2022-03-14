using System.Collections.Generic;

namespace SupplementStore.Domain.Opinions {

    public interface IOpinionRepository : IRepository<Opinion> {
        IEnumerable<Opinion> FindBy(IEnumerable<OpinionId> opinionIds);
    }
}
