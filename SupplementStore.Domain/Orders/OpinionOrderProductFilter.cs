using SupplementStore.Domain.Opinions;
using System.Linq;

namespace SupplementStore.Domain.Orders {

    public class OpinionOrderProductFilter : IFilter<OrderProduct> {

        public OpinionId OpinionId { get; }

        public OpinionOrderProductFilter(OpinionId opinionId) {

            OpinionId = opinionId;
        }

        public OrderProduct Process(IQueryable<OrderProduct> entities) {

            return entities.FirstOrDefault(e => e.OpinionId == OpinionId);
        }
    }
}
