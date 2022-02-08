namespace SupplementStore.Domain.Entities.Orders {

    static class OrderBusinessRules {

        public static readonly BusinessRule PostalCodeInvalidFormat = new BusinessRule(
            "PostalCode", "An order's postal code has invalid format.");
    }
}
