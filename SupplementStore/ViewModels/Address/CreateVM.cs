namespace SupplementStore.ViewModels.Address {

    public class CreateVM {

        [IsRequired]
        [Label]
        public string Street { get; set; }

        [IsRequired]
        [PostalCode]
        [Label]
        public string PostalCode { get; set; }

        [IsRequired]
        [Label]
        public string City { get; set; }
    }
}
