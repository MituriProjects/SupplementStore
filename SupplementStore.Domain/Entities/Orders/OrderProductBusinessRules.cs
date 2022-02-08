namespace SupplementStore.Domain.Entities.Orders {

    static class OrderProductBusinessRules {

        public static readonly BusinessRule QuantityAboveZeroRequired = new BusinessRule(
            "Quantity", "An order product's quantity has to be above zero.");
    }
}
