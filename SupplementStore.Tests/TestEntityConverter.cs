using SupplementStore.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Tests {

    static class TestEntityConverter {

        public static TEntity Process<TEntity>(TEntity entity)
            where TEntity : Entity {

            var testEntityType = SourceTestEntityType(entity);

            var testEntity = (TEntity)CreateInstance(testEntityType);

            CopyCorespondingValues(entity, testEntity);

            return testEntity;
        }

        private static Type SourceTestEntityType<TEntity>(TEntity entity)
            where TEntity : Entity {

            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.BaseType == typeof(TEntity))
                ?? throw new ArgumentException($"There is no TestEntity equivalent of {typeof(TEntity)} type.");
        }

        private static object CreateInstance(Type type) {

            return Activator.CreateInstance(type);
        }

        private static void CopyCorespondingValues<TEntity>(TEntity entity, TEntity testEntity)
            where TEntity : Entity {

            var propertyValues = TestEntityHelper.SelectPropertyValues(entity, testEntity).ToDictionary(p => "With" + p.Key, p => p.Value);

            var setterMethods = TestEntityHelper.SelectSetterMethods(testEntity);

            foreach (var setterMethod in setterMethods) {

                Invoke(setterMethod, testEntity, propertyValues[setterMethod.Name]);
            }

            EntityExtensions.WithId(testEntity, (Guid)propertyValues["WithId"]);
        }

        private static void Invoke(MethodInfo methodInfo, object obj, object value) {

            methodInfo.Invoke(obj, new object[] { value });
        }
    }
}
