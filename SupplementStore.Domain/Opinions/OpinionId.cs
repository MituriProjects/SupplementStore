using System;

namespace SupplementStore.Domain.Opinions {

    public class OpinionId : IdBase {

        public OpinionId(Guid id) : base(id) {
        }

        public OpinionId(string id) : base(id) {
        }
    }
}
