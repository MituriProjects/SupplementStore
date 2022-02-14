namespace SupplementStore.Domain.Entities.Products {

    public class Product : Entity {

        ProductName ProductName { get; set; }

        ProductPrice ProductPrice { get; set; }

        public string Name {
            get => ProductName.Value;
            set => ProductName = new ProductName(value);
        }

        public decimal Price {
            get => ProductPrice.Value;
            set => ProductPrice = new ProductPrice(value);
        }

        protected override void Validate() {
        }
    }
}
