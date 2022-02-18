using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.Configurations {

    class OrderConfiguration : EntityConfiguration<Order> {

        protected override void ConfigureEntity(EntityTypeBuilder<Order> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.OwnsOne(e => e.Address)
                .Property(e => e.Street)
                .IsRequired();
            builder.OwnsOne(e => e.Address)
                .Property(e => e.PostalCode)
                .IsRequired();
            builder.OwnsOne(e => e.Address)
                .Property(e => e.City)
                .IsRequired();
        }
    }
}
