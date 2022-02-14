using System;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Domain {

    public abstract class Entity {

        public Guid Id {
            get => GetEntityIdValue();
            set => SetEntityIdValue(value);
        }

        private Guid GetEntityIdValue() {

            var entityId = GetEntityIdProperty().GetValue(this);

            return (Guid)entityId.GetType()
                .BaseType
                .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entityId);
        }

        private void SetEntityIdValue(Guid id) {

            GetEntityIdProperty().SetValue(this, CreateEntityId(id));
        }

        private PropertyInfo GetEntityIdProperty() {

            return GetType()
                .GetProperty($"{GetType().Name}Id")
                ?? throw new NullReferenceException($"The '{GetType().FullName}' type does not have a dedicated entity id assigned.");
        }

        private object CreateEntityId(Guid id) {

            Type idType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.FullName == $"{GetType().FullName}Id")
                ?? throw new NullReferenceException($"No dedicated entity id for the '{GetType().FullName}' type was found.");

            return Activator.CreateInstance(idType, id);
        }
    }
}
