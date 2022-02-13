using System.Collections.Generic;

namespace SupplementStore.Domain.Entities {

    public interface IManyFilter<TEntity>
        where TEntity : Entity {

        IEnumerable<TEntity> Process(IEnumerable<TEntity> entities);
    }
}
