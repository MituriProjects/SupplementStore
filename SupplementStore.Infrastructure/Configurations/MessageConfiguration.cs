using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain.Messages;

namespace SupplementStore.Infrastructure.Configurations {

    class MessageConfiguration : EntityConfiguration<Message> {

        protected override void ConfigureEntity(EntityTypeBuilder<Message> builder) {

            builder.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
            builder.Property(e => e.Text)
                .IsRequired();
        }
    }
}
