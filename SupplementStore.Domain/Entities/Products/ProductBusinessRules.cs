namespace SupplementStore.Domain.Entities.Products {

    static class ProductBusinessRules {

        public static readonly BusinessRule NameRequired = new BusinessRule(
            "Name", "A product must have a name.");
        public static readonly BusinessRule PriceAboveZeroRequired = new BusinessRule(
            "Name", "A product's price has to be above zero.");
    }
}
