using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Entities.Orders;

namespace SupplementStore.Infrastructure.Configurations {

    class OrderConfiguration : EntityConfiguration<Order> {

        protected override void ConfigureEntity(EntityTypeBuilder<Order> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.Property(e => e.Address)
                .IsRequired();
            builder.Property(e => e.PostalCode)
                .IsRequired();
            builder.Property(e => e.City)
                .IsRequired();
        }
    }
}
