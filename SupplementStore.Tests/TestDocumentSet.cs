using SupplementStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Tests {

    static class TestDocumentSet {

        static List<IClearableDocument> Documents { get; } = new List<IClearableDocument>();

        static Dictionary<Type, Func<IEnumerable<IEnumerable<object>>>> EntityValues { get; } = new Dictionary<Type, Func<IEnumerable<IEnumerable<object>>>>();

        public static void Register<TEntity>(IClearableDocument<TEntity> document)
            where TEntity : Entity {

            if (EntityValues.TryAdd(typeof(TEntity), () => document.All.Select(e => GetEntityValues(e)))) {

                Documents.Add(document);
            }
        }

        public static void ClearDocuments() {

            Documents.ForEach(d => d.Clear());

            Documents.Clear();

            EntityValues.Clear();
        }

        public static IEnumerable<object> GetValues() {

            return EntityValues
                .Values
                .Select(v => v())
                .SelectMany(e => e)
                .SelectMany(e => e)
                .ToList();
        }

        private static IEnumerable<object> GetEntityValues<TEntity>(TEntity entity)
            where TEntity : Entity {

            return TestEntityHelper
                .SelectPropertyValues(entity)
                .Select(p => p.Value);
        }
    }
}
