using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Domain {

    public interface IManyFilter<TEntity>
        where TEntity : Entity {

        IEnumerable<TEntity> Process(IQueryable<TEntity> entities);
    }
}
