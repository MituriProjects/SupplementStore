using SupplementStore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure {

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity {

        protected IDocument<TEntity> Document { get; }

        public Repository(IDocument<TEntity> document) {

            Document = document;
        }

        public IEnumerable<TEntity> Entities => Document.All;

        public int Count() => Document.All.Count();

        public void Add(TEntity entity) {

            Document.Add(entity);
        }

        public TEntity FindBy(IFilter<TEntity> filter) {

            return filter.Process(Document.All);
        }

        public IEnumerable<TEntity> FindBy(IManyFilter<TEntity> filter) {

            return filter.Process(Document.All);
        }
    }
}
