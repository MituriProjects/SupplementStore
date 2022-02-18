using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class BasketProductConfiguration : EntityConfiguration<BasketProduct> {

        protected override void ConfigureEntity(EntityTypeBuilder<BasketProduct> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            builder.Ignore(e => e.ProductId);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("Product_Id")
                .IsRequired();
        }
    }
}
