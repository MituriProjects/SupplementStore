using SupplementStore.Application.Args;
using SupplementStore.Domain.Addresses;
using SupplementStore.Infrastructure.ArgsMapping;

namespace SupplementStore.Infrastructure.AppServices.Addresses {

    public partial class AddressService {

        public void Create(AddressCreateArgs args) {

            AddressFactory.Create(args.ToFactoryArgs());

            DomainApprover.SaveChanges();
        }
    }
}
