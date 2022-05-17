namespace SupplementStore.ViewModels.Product {

    public class ProductEditVM {

        public string Id { get; set; }

        [IsRequired]
        [Label]
        public string Name { get; set; }

        [IsRequired]
        [Price]
        [Label]
        public decimal? Price { get; set; } = 0;
    }
}
