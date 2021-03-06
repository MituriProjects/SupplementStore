using SupplementStore.Domain;
using SupplementStore.Infrastructure;

namespace SupplementStore.Tests {

    interface IClearableDocument {
        void Clear();
    }

    interface IClearableDocument<TEntity> : IDocument<TEntity>, IClearableDocument
        where TEntity : Entity {
    }
}
