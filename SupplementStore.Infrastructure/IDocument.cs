using System;
using System.Linq;

namespace SupplementStore.Infrastructure {

    public interface IDocument<TEntity> {

        IQueryable<TEntity> All { get; }

        void Add(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entity);
    }
}
