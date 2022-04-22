using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.Configurations {

    class OrderConfiguration : EntityConfiguration<Order> {

        protected override void ConfigureEntity(EntityTypeBuilder<Order> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.Ignore(e => e.AddressId);
            builder.HasOne<Domain.Addresses.Address>()
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey("Address_Id")
                .IsRequired();
        }
    }
}
