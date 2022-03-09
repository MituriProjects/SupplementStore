using SupplementStore.Domain.Opinions;

namespace SupplementStore.Infrastructure.Repositories {

    public class OpinionRepository : Repository<Opinion>, IOpinionRepository {

        public OpinionRepository(IDocument<Opinion> document) : base(document) {
        }
    }
}
