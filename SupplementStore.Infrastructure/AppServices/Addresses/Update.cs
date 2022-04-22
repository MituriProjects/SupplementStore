using SupplementStore.Application.Args;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService {

        public void Update(AddressUpdateArgs args) {

            var address = AddressRepository.FindBy(new AddressId(args.Id));

            if (address == null)
                return;

            if (address.UserId != args.UserId)
                return;

            address.Street = args.Street;
            address.PostalCode = new PostalCode(args.PostalCode);
            address.City = args.City;

            DomainApprover.SaveChanges();
        }
    }
}
