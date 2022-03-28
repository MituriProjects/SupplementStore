using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;

namespace SupplementStore.Infrastructure.Configurations {

    class OpinionConfiguration : EntityConfiguration<Opinion> {

        protected override void ConfigureEntity(EntityTypeBuilder<Opinion> builder) {

            builder.Ignore(e => e.PurchaseId);
            builder.HasOne<Purchase>()
                .WithOne()
                .HasForeignKey<Opinion>("Purchase_Id")
                .IsRequired();
            builder.OwnsOne(e => e.Rating);
        }
    }
}
