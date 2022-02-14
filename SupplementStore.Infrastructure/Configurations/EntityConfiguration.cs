using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplementStore.Domain;

namespace SupplementStore.Infrastructure.Configurations {

    abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity {

        public void Configure(EntityTypeBuilder<TEntity> builder) {

            builder.Ignore($"{typeof(TEntity).Name}Id");
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()");

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
