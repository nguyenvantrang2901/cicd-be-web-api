using System;
using APIs_Manager_Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace APIs_Manager_Inventory.Data
{
    public class HomeDBContext : DbContext
    {
        

        //Create DB Set
        public DbSet<UserInfo> Sys_User { get; set; }
        public DbSet<HeaderItem> Sys_HeaderItem { get; set; }
        public DbSet<Sidebar> Sys_Sidebar { get; set; }
        public DbSet<SidebarItem> Sys_SidebarItem { get; set; }
        public DbSet<Delivery> Sys_Delivery { get; set; }
        public DbSet<Product> Sys_Product { get; set; }
        public DbSet<Order> Sys_Order { get; set; }
        public DbSet<OrderItem> Sys_OrderItem { get; set; }
        public HomeDBContext(DbContextOptions<HomeDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sidebar>()
                .HasMany(s => s.Children)
                .WithOne(c => c.Sidebar)
                .HasForeignKey(c => c.SidebarId)
                .OnDelete(DeleteBehavior.Cascade); // Xoá Sidebar thì xoá luôn con
            // NEW: liên kết Sidebar ↔ HeaderItem
            modelBuilder.Entity<Sidebar>()
                .HasOne(s => s.Header)
                .WithMany() // hoặc WithMany(h => h.Sidebars) nếu bạn muốn điều hướng ngược
                .HasForeignKey(s => s.HeaderItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasMany(o => o.Items)
               .WithOne()
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithMany()
                .HasForeignKey(o => o.DeliveryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
