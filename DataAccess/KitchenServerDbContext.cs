using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;


namespace DAL
{
    public class KitchenServerDbContext : DbContext
    {
        public KitchenServerDbContext()
        {
        }
        public KitchenServerDbContext(DbContextOptions<KitchenServerDbContext> options) : base(options) { }
 
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PrintOrderGroup> PrintOrder { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Models.Monitor> Monitor { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<Department> Department { get; set; }
		public DbSet<Device> Device { get; set; }
		public DbSet<Cancellation> Cancellation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderId,  o.OrderLineNo });
            modelBuilder.Entity<OrderItem>().HasOne<Order>(oi => oi.Order).WithMany(order => order.OrderItems).HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<Cancellation>().HasOne<OrderItem>(c => c.OrderItem).WithMany(order => order.Cancellations).HasForeignKey(c=> new { c.OrderId, c.OrderLineNo });
        }
    }
}