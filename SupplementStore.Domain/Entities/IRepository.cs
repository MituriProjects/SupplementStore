using System;
using System.Collections.Generic;

namespace SupplementStore.Domain.Entities {

    public interface IRepository<TEntity>
        where TEntity : Entity {

        IEnumerable<TEntity> Entities { get; }
        int Count();

        void Add(TEntity entity);
        void Delete(Guid id);

        TEntity FindBy(Guid id);
        TEntity FindBy(IFilter<TEntity> filter);
        IEnumerable<TEntity> FindBy(IManyFilter<TEntity> filter);
    }
}
