namespace SupplementStore.Domain.Entities.Products {

    public class Product : Entity {

        ProductName ProductName { get; set; }

        public string Name {

            get => ProductName.Value;
            set => ProductName = new ProductName(value);
        }

        public decimal Price { get; set; }

        protected override void Validate() {

            if (Price <= 0)
                AddBrokenRule(ProductBusinessRules.PriceAboveZeroRequired);
        }
    }
}
