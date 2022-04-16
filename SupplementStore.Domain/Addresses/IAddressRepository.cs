namespace SupplementStore.Domain.Addresses {

    public interface IAddressRepository : IRepository<Address> {
        Address FindBy(AddressId addressId);
    }
}
