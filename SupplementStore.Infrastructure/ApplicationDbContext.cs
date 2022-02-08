using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplementStore.Domain.Entities.Baskets;
using SupplementStore.Domain.Entities.Orders;
using SupplementStore.Domain.Entities.Products;
using SupplementStore.Infrastructure.Configurations;

namespace SupplementStore.Infrastructure {

    public class ApplicationDbContext : IdentityDbContext<IdentityUser> {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<BasketProduct> BasketProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new BasketProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
