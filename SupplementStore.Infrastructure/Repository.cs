using SupplementStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure {

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity {

        IDocument<TEntity> Document { get; }

        public Repository(IDocument<TEntity> document) {

            Document = document;
        }

        public IEnumerable<TEntity> Entities => throw new NotImplementedException();

        public void Add(TEntity entity) {

            Document.Add(entity);
        }

        public void Delete(Guid id) {

            Document.Delete(id);
        }

        public TEntity FindBy(Guid id) {

            return Document.All
                .FirstOrDefault(e => e.Id == id);
        }

        public TEntity FindBy(IFilter<TEntity> filter) {

            return filter.Process(Document.All);
        }

        public IEnumerable<TEntity> FindBy(IManyFilter<TEntity> filter) {

            return filter.Process(Document.All);
        }
    }
}
