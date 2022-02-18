using SupplementStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Tests {

    static class TestEntityHelper {

        public static IEnumerable<MethodInfo> SelectSetterMethods<TEntity>(TEntity entity)
            where TEntity : Entity {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name == $"{entity.GetType().Name}Extensions" || t.Name == "EntityExtensions")
                .Select(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .SelectMany(m => m)
                .Where(m => m.Name.StartsWith("With"))
                .ToList();
        }

        public static IEnumerable<KeyValuePair<string, object>> SelectPropertyValues<TEntity>(TEntity entity)
            where TEntity : Entity {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var setterMethods = SelectSetterMethods(entity);

            foreach (var setterMethod in setterMethods) {

                var propertyName = setterMethod.Name.Substring(4);

                if (propertyName == "Id")
                    continue;

                var propertyValue = entity
                    .GetType()
                    .GetProperty(propertyName)
                    .GetValue(entity);

                yield return new KeyValuePair<string, object>(propertyName, propertyValue);
            }

            yield return new KeyValuePair<string, object>("Id", entity.GetId());
        }
    }
}
