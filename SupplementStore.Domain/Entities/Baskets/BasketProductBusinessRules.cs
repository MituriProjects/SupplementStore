namespace SupplementStore.Domain.Entities.Baskets {

    static class BasketProductBusinessRules {

        public static readonly BusinessRule QuantityAboveZeroRequired = new BusinessRule(
            "Quantity", "A basket product's quantity has to be above zero.");
    }
}
