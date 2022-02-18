﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;

namespace SupplementStore.Infrastructure.Configurations {

    class OrderProductConfiguration : EntityConfiguration<OrderProduct> {

        protected override void ConfigureEntity(EntityTypeBuilder<OrderProduct> builder) {

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
        }
    }
}