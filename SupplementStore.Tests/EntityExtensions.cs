using SupplementStore.Domain;
using System;

namespace SupplementStore.Tests {

    static class EntityExtensions {

        public static TEntity WithId<TEntity>(this TEntity entity, Guid id)
            where TEntity : Entity {

            entity.Id = id;

            return entity;
        }
    }
}
