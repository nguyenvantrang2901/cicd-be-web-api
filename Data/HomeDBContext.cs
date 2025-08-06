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
        }
    }
}
