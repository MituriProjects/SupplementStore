using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService {

        public void Hide(string userId, string addressId) {

            var address = AddressRepository.FindBy(new AddressId(addressId));

            if (address == null)
                return;

            if (address.UserId != userId)
                return;

            address.IsHidden = true;

            DomainApprover.SaveChanges();
        }
    }
}
