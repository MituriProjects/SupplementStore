using System.Linq;

namespace SupplementStore.Domain.Addresses {

    public class UserWholeAddressFilter : IFilter<Address> {

        string UserId { get; }

        string Street { get; }

        PostalCode PostalCode { get; }

        string City { get; }

        public UserWholeAddressFilter(
            string userId,
            string street,
            PostalCode postalCode,
            string city) {

            UserId = userId;
            Street = street;
            PostalCode = postalCode;
            City = city;
        }

        public Address Process(IQueryable<Address> entities) {

            return entities.FirstOrDefault(e =>
                e.UserId == UserId
                && e.Street == Street
                && e.PostalCode == PostalCode
                && e.City == City
            );
        }
    }
}
