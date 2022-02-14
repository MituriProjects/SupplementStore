using SupplementStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Tests {

    static class TestEntityHelper {

        public static IEnumerable<MethodInfo> SelectSetterMethods<TEntity>(TEntity testEntity)
            where TEntity : Entity {

            if (testEntity == null)
                throw new ArgumentNullException(nameof(testEntity));

            return testEntity
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name.StartsWith("With"));
        }

        public static IEnumerable<KeyValuePair<string, object>> SelectPropertyValues<TEntity>(TEntity entity)
            where TEntity : Entity {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var setterMethods = SelectSetterMethods(entity);

            foreach (var setterMethod in setterMethods) {

                var propertyName = setterMethod.Name.Substring(4);

                var propertyValue = entity
                    .GetType()
                    .GetProperty(propertyName)
                    .GetValue(entity);

                yield return new KeyValuePair<string, object>(propertyName, propertyValue);
            }

            yield return new KeyValuePair<string, object>("Id", entity.Id);
        }
    }
}
