using Microsoft.EntityFrameworkCore;
using WildBeard.Orders.Model;

namespace WildBeard.Orders.InfraServices
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<OrderDeliveryAddress> OrderDeliveryAddresses { get; set; }

        public DbSet<OrderLineCancellation> OrderLineCancellations { get; set; }

        public DbSet<OrderBillingAddress> OrderBillingAddresses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Initial Catalog=Orders_Api;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>()
                        .HasOne(c => c.Order)
                        .WithMany(c => c.OrderLines)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderLine>()
                        .HasOne(o => o.Cancellation)
                        .WithMany(o => o.OrderLines)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDeliveryAddress>().HasOne(a => a.Order).WithOne(a => a.DeliveryAddress).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderBillingAddress>().HasOne(a => a.Order).WithOne(a => a.BillingAddress).OnDelete(DeleteBehavior.Cascade);
        }
    }
}