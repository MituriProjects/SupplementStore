using System;
using System.Collections.Generic;

namespace SupplementStore.Domain {

    public abstract class IdBase : ValueObject<IdBase> {

        Guid Id { get; set; }

        protected IdBase(Guid id) {

            Id = id;
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Id
            };
        }
    }
}
