using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class OrderProductConfiguration : EntityConfiguration<OrderProduct> {

        protected override void ConfigureEntity(EntityTypeBuilder<OrderProduct> builder) {

            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(e => e.OrderId)
                .IsRequired();
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired();
        }
    }
}
