using System.Linq;

namespace SupplementStore.Domain.Entities {

    public interface IFilter<TEntity>
        where TEntity : Entity {

        TEntity Process(IQueryable<TEntity> entities);
    }
}
