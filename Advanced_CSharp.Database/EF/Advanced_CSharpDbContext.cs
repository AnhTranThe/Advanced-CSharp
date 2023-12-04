using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Database.EF
{
    public class AdvancedCSharpDbContext : DbContext
    {
        public AdvancedCSharpDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<AppUserRole>()
                .HasKey(e => new { e.UserId, e.RoleId });
            _ = modelBuilder.Entity<CartDetail>()
              .HasKey(e => new { e.CartId, e.ProductId });
            _ = modelBuilder.Entity<OrderDetail>()
           .HasKey(e => new { e.OrderId, e.ProductId });
            base.OnModelCreating(modelBuilder);
        }

        public virtual Task<int> SaveChangesAsync(string username = "")
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = username;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = username;
                }
            }
            return base.SaveChangesAsync();
        }



        public DbSet<Cart>? Carts { get; set; }
        public DbSet<CartDetail>? CartDetails { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrdersDetail { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<AppRole>? AppRoles { get; set; }
        public DbSet<AppUserRole>? AppUserRoles { get; set; }
        public DbSet<AppVersion>? AppVersions { get; set; }

    }
}
