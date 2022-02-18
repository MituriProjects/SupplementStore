using System.Collections.Generic;

namespace SupplementStore.Domain {

    public interface IRepository<TEntity>
        where TEntity : Entity {

        IEnumerable<TEntity> Entities { get; }
        int Count();

        void Add(TEntity entity);

        TEntity FindBy(IFilter<TEntity> filter);
        IEnumerable<TEntity> FindBy(IManyFilter<TEntity> filter);
    }
}
