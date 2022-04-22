using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain;
using SupplementStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Tests {

    class TestDocument<TEntity> : IClearableDocument<TEntity>
        where TEntity : Entity {

        static List<TEntity> Entities { get; set; } = new List<TEntity> { };

        public IQueryable<TEntity> All => Entities.AsQueryable();

        public TestDocument() {

            TestDocumentSet.Register(this);
        }

        void IDocument<TEntity>.Add(TEntity entity) {

            entity.SetId(Guid.NewGuid());

            Entities.Add(entity);
        }

        public void Delete(TEntity entity) {

            Entities.Remove(entity);
        }

        public void Clear() {

            Entities = new List<TEntity>();
        }

        public static void Add(TEntity entity) {

            Entities.Add(entity);
        }

        public static TEntity First(Func<TEntity, bool> predicate) {

            return Entities.First(predicate);
        }

        public static void IsEmpty() {

            if (Entities.Count > 0) {

                throw new AssertFailedException($"The TestDocument<{typeof(TEntity).Name}> contains at least one element.");
            }
        }

        public static TEntity Single(Func<TEntity, bool> predicate) {

            var entities = Entities
                .Where(predicate)
                .ToList();

            if (entities.Count == 0) {

                throw new AssertFailedException($"The TestDocument<{typeof(TEntity).Name}> does not contain a single element that matches the provided predicate.");
            }
            else if (entities.Count > 1) {

                throw new AssertFailedException($"The TestDocument<{typeof(TEntity).Name}> contains more than one element that matches the provided predicate.");
            }

            return entities.First();
        }

        public static void None(Func<TEntity, bool> predicate) {

            var entity = Entities
                .FirstOrDefault(predicate);

            if (entity != null) {

                throw new AssertFailedException($"The TestDocument<{typeof(TEntity).Name}> contains at least one matching element.");
            }
        }
    }
}
