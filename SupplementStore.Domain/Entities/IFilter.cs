using System.Collections.Generic;

namespace SupplementStore.Domain.Entities {

    public interface IFilter<TEntity>
        where TEntity : Entity {

        TEntity Process(IEnumerable<TEntity> entities);
    }
}
