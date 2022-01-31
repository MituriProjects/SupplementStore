using System;
using System.Collections.Generic;

namespace SupplementStore.Domain.Entities {

    public abstract class Entity {

        public Guid Id { get; set; }

        private List<BusinessRule> BrokenRules { get; } = new List<BusinessRule>();

        public IEnumerable<BusinessRule> GetBrokenRules() {

            BrokenRules.Clear();

            Validate();

            return BrokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule) {

            BrokenRules.Add(businessRule);
        }


        protected abstract void Validate();
    }
}
