using SupplementStore.Domain;
using SupplementStore.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Tests {

    static class TestEntity {

        public static TEntity Random<TEntity>() {

            var entity = Create<TEntity>();

            AssignId(entity);
            AssignPropertyValues(entity);
            AddToDocument(entity);

            return entity;
        }

        public static TEntity[] Random<TEntity>(int count) {

            return new TEntity[count]
                .Select(e => Random<TEntity>())
                .ToArray();
        }

        private static TEntity Create<TEntity>() {

            return Activator.CreateInstance<TEntity>();
        }

        private static void AssignId(object entity) {

            var id = Guid.NewGuid();

            entity.GetType().GetProperty("Id").SetValue(entity, id);
        }

        private static void AssignPropertyValues(object entity) {

            var propertyActions = new Dictionary<Type, object> {
                { typeof(Guid), Guid.NewGuid() },
                { typeof(int), RandomManager.Next(100) },
                { typeof(decimal), RandomManager.Next(10000) / 100M }
            };

            foreach (var property in entity.GetType().GetProperties()) {

                if (property.Name == "Id")
                    continue;

                if (property.Name == "UserId") {

                    property.SetValue(entity, Guid.NewGuid().ToString());

                    continue;
                }

                if (property.PropertyType == typeof(string)) {

                    property.SetValue(entity, $"{entity.GetType().Name}-{property.Name}-{entity.GetType().GetProperty("Id").GetValue(entity)}");

                    continue;
                }

                if (property.PropertyType.BaseType.IsGenericType
                    && property.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(ValueObject<>)) {

                    var constructor = typeof(Address).GetConstructors()[0];

                    var parameters = constructor.GetParameters();

                    var parameterValues = new List<object>();

                    foreach (var parameter in parameters) {

                        parameterValues.Add($"{entity.GetType().Name}-{property.Name}-{parameter.Name[0].ToString().ToUpper() + parameter.Name.Substring(1)}-{entity.GetType().GetProperty("Id").GetValue(entity)}");
                    }

                    var obj = constructor.Invoke(parameterValues.ToArray());

                    property.SetValue(entity, obj);

                    continue;
                }

                if (propertyActions.ContainsKey(property.PropertyType))
                    property.SetValue(entity, propertyActions[property.PropertyType]);
            }
        }

        private static void AddToDocument(object entity) {

            typeof(TestDocument<>)
                .MakeGenericType(entity.GetType())
                .GetMethod("Add")
                .Invoke(null, new object[] { entity });
        }
    }
}
