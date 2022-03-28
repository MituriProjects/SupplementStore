using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplementStore.Domain.Baskets;
using SupplementStore.Domain.Opinions;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using SupplementStore.Domain.Wishes;
using SupplementStore.Infrastructure.Configurations;

namespace SupplementStore.Infrastructure {

    public class ApplicationDbContext : IdentityDbContext<IdentityUser> {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<BasketProduct> BasketProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public DbSet<Opinion> Opinions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new BasketProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new PurchaseConfiguration());
            builder.ApplyConfiguration(new WishConfiguration());
            builder.ApplyConfiguration(new OpinionConfiguration());
        }
    }
}
