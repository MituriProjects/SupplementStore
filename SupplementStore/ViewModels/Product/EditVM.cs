namespace SupplementStore.ViewModels.Product {

    public class EditVM {

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
