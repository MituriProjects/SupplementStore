using SupplementStore.Domain.Opinions;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OpinionPurchaseFilter : IFilter<Purchase> {

        public OpinionId OpinionId { get; }

        public OpinionPurchaseFilter(OpinionId opinionId) {

            OpinionId = opinionId;
        }

        public Purchase Process(IQueryable<Purchase> entities) {

            return entities.FirstOrDefault(e => e.OpinionId == OpinionId);
        }
    }
}
