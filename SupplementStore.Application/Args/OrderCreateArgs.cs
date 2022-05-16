namespace SupplementStore.Application.Args {

    public class OrderCreateArgs {

        public string UserId { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public bool ShouldAddressBeHidden { get; set; }
    }
}
