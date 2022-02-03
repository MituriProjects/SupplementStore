namespace SupplementStore.Domain {

    public class BusinessRule {

        public string Property { get; }

        public string Rule { get; }

        public BusinessRule(string property, string rule) {

            Property = property;
            Rule = rule;
        }
    }
}
