using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Addresses;

namespace SupplementStore.Infrastructure.Configurations {

    class AddressConfiguration : EntityConfiguration<Address> {

        protected override void ConfigureEntity(EntityTypeBuilder<Address> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.Property(e => e.Street)
                .IsRequired();
            builder.OwnsOne(e => e.PostalCode)
                .Property(e => e.Value)
                .HasColumnName("PostalCode")
                .IsRequired();
            builder.Property(e => e.City)
                .IsRequired();
        }
    }
}
