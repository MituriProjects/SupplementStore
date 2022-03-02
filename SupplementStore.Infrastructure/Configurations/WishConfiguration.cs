using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;

namespace SupplementStore.Infrastructure.Configurations {

    class WishConfiguration : EntityConfiguration<Wish> {

        protected override void ConfigureEntity(EntityTypeBuilder<Wish> builder) {

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
