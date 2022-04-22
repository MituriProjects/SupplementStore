namespace SupplementStore.Domain.Addresses {

    public class AddressFactory {

        IAddressRepository AddressRepository { get; }

        public AddressFactory(IAddressRepository addressRepository) {

            AddressRepository = addressRepository;
        }

        public Address Create(AddressFactoryArgs args) {

            var address = new Address {
                UserId = args.UserId,
                Street = args.Street,
                PostalCode = new PostalCode(args.PostalCode),
                City = args.City,
                IsHidden = false
            };

            AddressRepository.Add(address);

            return address;
        }
    }
}
