using System;
using System.Collections.Generic;

namespace SupplementStore.Domain {

    public abstract class IdBase : ValueObject<IdBase> {

        internal Guid Id { get; set; }

        public bool IsEmpty => Id == Guid.Empty;

        protected IdBase(Guid id) {

            Id = id;
        }

        protected IdBase(string id) {

            if (Guid.TryParse(id, out var guidId))
                Id = guidId;
            else
                Id = Guid.Empty;
        }

        protected override IEnumerable<object> GetValues() {

            return new object[] {
                Id
            };
        }

        public override string ToString() {

            return Id.ToString();
        }
    }
}
