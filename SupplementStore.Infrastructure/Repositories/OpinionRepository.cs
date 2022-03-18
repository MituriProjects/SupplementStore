using SupplementStore.Domain.Opinions;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.Repositories {

    public class OpinionRepository : Repository<Opinion>, IOpinionRepository {

        public OpinionRepository(IDocument<Opinion> document) : base(document) {
        }

        public Opinion FindBy(OpinionId opinionId) {

            return Document.All
                .FirstOrDefault(e => e.OpinionId == opinionId);
        }

        public IEnumerable<Opinion> FindBy(IEnumerable<OpinionId> opinionIds) {

            return Document.All
                .Where(e => opinionIds.Contains(e.OpinionId))
                .ToList();
        }
    }
}
