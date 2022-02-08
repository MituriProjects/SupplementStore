using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Tests {

    static class TestEntity {

        public static TTestEntity Random<TTestEntity>() {

            var testEntity = Create<TTestEntity>();

            AssignId(testEntity);
            AssignPropertyValues(testEntity);
            AddToDocument(testEntity);

            return testEntity;
        }

        public static TTestEntity[] Random<TTestEntity>(int count) {

            return new TTestEntity[count]
                .Select(e => Random<TTestEntity>())
                .ToArray();
        }

        private static TTestEntity Create<TTestEntity>() {

            return Activator.CreateInstance<TTestEntity>();
        }

        private static void AssignId(object testEntity) {

            var id = Guid.NewGuid();

            testEntity.GetType().GetProperty("Id").SetValue(testEntity, id);
        }

        private static void AssignPropertyValues(object testEntity) {

            var propertyActions = new Dictionary<Type, Action<PropertyInfo>> {
                { typeof(string), p => p.SetValue(testEntity, $"{testEntity.GetType().Name}{p.Name}-{testEntity.GetType().GetProperty("Id").GetValue(testEntity)}") },
                { typeof(Guid), p => p.SetValue(testEntity, Guid.NewGuid()) },
                { typeof(int), p => p.SetValue(testEntity, RandomManager.Next(100)) },
                { typeof(decimal), p => p.SetValue(testEntity, RandomManager.Next(10000) / 100M) }
            };

            foreach (var property in testEntity.GetType().GetProperties()) {

                if (property.Name == "Id")
                    continue;

                if (property.Name == "UserId") {

                    property.SetValue(testEntity, Guid.NewGuid().ToString());

                    continue;
                }

                if (propertyActions.ContainsKey(property.PropertyType))
                    propertyActions[property.PropertyType](property);
            }
        }

        private static void AddToDocument(object testEntity) {

            var baseType = testEntity.GetType().BaseType;

            typeof(TestDocument<>)
                .MakeGenericType(baseType)
                .GetMethod("Add")
                .Invoke(null, new object[] { testEntity });
        }
    }
}
