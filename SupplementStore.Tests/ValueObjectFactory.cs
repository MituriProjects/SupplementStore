using SupplementStore.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace SupplementStore.Tests {

    static class ValueObjectFactory {

        public static object Create(Type valueObjectType, object entity) {

            if ((entity is Entity) == false)
                throw new ArgumentException("A ValueObjectFactory's Create's entity argument must implement the Entity type.");

            var factory = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(a => a.Name == $"{valueObjectType.Name}Factory");

            if (factory == null)
                throw new NotImplementedException($"No factory for the {valueObjectType.FullName} type was detected.");

            return factory.GetMethod("Create")
                .Invoke(null, new object[] { entity });
        }
    }
}
