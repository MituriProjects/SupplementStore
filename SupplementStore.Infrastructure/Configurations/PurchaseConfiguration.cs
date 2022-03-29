using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class PurchaseConfiguration : EntityConfiguration<Purchase> {

        protected override void ConfigureEntity(EntityTypeBuilder<Purchase> builder) {

            builder.Ignore(e => e.OrderId);
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey("Order_Id")
                .IsRequired();
            builder.Ignore(e => e.ProductId);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("Product_Id")
                .IsRequired();
            builder.Ignore(e => e.OpinionId);
            builder.HasOne<Opinion>()
                .WithOne()
                .HasForeignKey<Purchase>("Opinion_Id");
        }
    }
}
