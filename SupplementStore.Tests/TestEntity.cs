using SupplementStore.Domain;
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

            ((Entity)entity).SetId(Guid.NewGuid());
        }

        private static void AssignPropertyValues(object entity) {

            var propertyActions = new Dictionary<Type, object> {
                { typeof(Guid), Guid.NewGuid() },
                { typeof(Guid?), null },
                { typeof(int), RandomManager.Next(100) },
                { typeof(decimal), RandomManager.Next(10000) / 100M },
                { typeof(DateTime), DateTime.Now.AddSeconds(-RandomManager.Next(10000000)) }
            };

            foreach (var property in entity.GetType().GetProperties()) {

                if (property.Name == "Id")
                    continue;

                if (property.Name == "UserId") {

                    property.SetValue(entity, Guid.NewGuid().ToString());

                    continue;
                }

                if (property.PropertyType == typeof(string)) {

                    property.SetValue(entity, $"{entity.GetType().Name}-{property.Name}-{((Entity)entity).GetId()}");

                    continue;
                }

                if (property.PropertyType.BaseType.IsGenericType
                    && property.PropertyType.BaseType.GetGenericTypeDefinition() == typeof(ValueObject<>)) {

                    var valueObject = ValueObjectFactory.Create(property.PropertyType, entity);

                    property.SetValue(entity, valueObject);

                    continue;
                }

                if (property.PropertyType.BaseType == typeof(IdBase)) {

                    property.SetValue(entity, Activator.CreateInstance(property.PropertyType, propertyActions[typeof(Guid)]));

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
