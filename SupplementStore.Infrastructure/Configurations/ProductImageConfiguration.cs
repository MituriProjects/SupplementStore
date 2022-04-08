using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class ProductImageConfiguration : EntityConfiguration<ProductImage> {

        protected override void ConfigureEntity(EntityTypeBuilder<ProductImage> builder) {

            builder.Ignore(e => e.ProductId);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("Product_Id")
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired();
        }
    }
}
