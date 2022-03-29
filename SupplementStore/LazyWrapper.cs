using Microsoft.Extensions.DependencyInjection;
using System;

namespace SupplementStore {

    class LazyWrapper<T> : Lazy<T> where T : class {

        public LazyWrapper(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>()) {
        }
    }
}
