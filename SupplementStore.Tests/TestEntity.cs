using SupplementStore.Domain;
using SupplementStore.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var propertyActions = new Dictionary<Type, object> {
                { typeof(Guid), Guid.NewGuid() },
                { typeof(int), RandomManager.Next(100) },
                { typeof(decimal), RandomManager.Next(10000) / 100M }
            };

            foreach (var property in testEntity.GetType().GetProperties()) {

                if (property.Name == "Id")
                    continue;

                if (property.Name == "UserId") {

                    property.SetValue(testEntity, Guid.NewGuid().ToString());

                    continue;
                }

                if (property.PropertyType == typeof(string)) {

                    property.SetValue(testEntity, $"{testEntity.GetType().Name}-{property.Name}-{testEntity.GetType().GetProperty("Id").GetValue(testEntity)}");

                    continue;
                }

                if (property.PropertyType.BaseType.IsGenericType
                    && property.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(ValueObject<>)) {

                    var constructor = typeof(Address).GetConstructors()[0];

                    var parameters = constructor.GetParameters();

                    var parameterValues = new List<object>();

                    foreach (var parameter in parameters) {

                        parameterValues.Add($"{testEntity.GetType().Name}-{property.Name}-{parameter.Name[0].ToString().ToUpper() + parameter.Name.Substring(1)}-{testEntity.GetType().GetProperty("Id").GetValue(testEntity)}");
                    }

                    var obj = constructor.Invoke(parameterValues.ToArray());

                    property.SetValue(testEntity, obj);

                    continue;
                }

                if (propertyActions.ContainsKey(property.PropertyType))
                    property.SetValue(testEntity, propertyActions[property.PropertyType]);
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
