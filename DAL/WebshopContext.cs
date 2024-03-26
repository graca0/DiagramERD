using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WebshopContext:DbContext
    {
        public DbSet<BasketPosition> BasketPosition { get; set; }
        public DbSet<Order> Order{ get; set; }
        public DbSet<Product> Product{ get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZadanieDomoweBazyDanych;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketPosition>()
                .HasOne(x => x.Product)
                .WithMany(x => x.BasketPositions)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasketPosition>()
                .HasMany(x=>x.Users)
                .WithOne(x=>x.BasketPosition)
                .HasForeignKey(x=>x.BasketPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.ProductGroup)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.GroupID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x=>x.Orders)
                .WithOne(x=>x.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
              .HasOne(x=>x.UserGroups)
              .WithMany(x=>x.Users)
              .HasForeignKey(x=>x.UserGroupId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderPosition>()
                .HasOne(x=>x.Order)
                .WithMany(x=>x.OrderPositions)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductGroup>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(x=>x.Id);
        }
    }
}
