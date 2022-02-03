namespace SupplementStore.Domain.Entities.Products {

    public class Product : Entity {

        public string Name { get; set; }

        public decimal Price { get; set; }

        protected override void Validate() {

            if (string.IsNullOrEmpty(Name))
                AddBrokenRule(ProductBusinessRules.NameRequired);
            if (Price <= 0)
                AddBrokenRule(ProductBusinessRules.PriceAboveZeroRequired);
        }
    }
}
