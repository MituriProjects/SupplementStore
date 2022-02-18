using System.Linq;

namespace SupplementStore.Domain {

    public interface IFilter<TEntity>
        where TEntity : Entity {

        TEntity Process(IQueryable<TEntity> entities);
    }
}
