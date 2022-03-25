using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain.Opinions {

    public class HiddenOpinionsFilter : IManyFilter<Opinion> {

        public IEnumerable<Opinion> Process(IQueryable<Opinion> entities) {

            return entities.Where(e => e.IsHidden);
        }
    }
}
