using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class ProductConfiguration : EntityConfiguration<Product> {

        protected override void ConfigureEntity(EntityTypeBuilder<Product> builder) {

            builder.Property(e => e.Name)
                .IsRequired();
        }
    }
}
