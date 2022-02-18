using SupplementStore.Domain;
using System;
using System.Reflection;

namespace SupplementStore.Tests {

    static class EntityExtensions {

        public static object GetId(this Entity entity) {

            return typeof(Entity)
                .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(entity);
        }

        public static void SetId(this Entity entity, Guid id) {

            typeof(Entity)
                .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(entity, id);
        }
    }
}
