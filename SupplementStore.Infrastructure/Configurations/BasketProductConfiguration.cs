using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class BasketProductConfiguration : EntityConfiguration<BasketProduct> {

        protected override void ConfigureEntity(EntityTypeBuilder<BasketProduct> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired();
        }
    }
}
