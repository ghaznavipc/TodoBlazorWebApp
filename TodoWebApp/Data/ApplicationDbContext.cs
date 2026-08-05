using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data.Models;

namespace TodoWebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //// DbSets for your entities
    public DbSet<TodoItem> TodoItems { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // Configure entity relationships and constraints
    //    modelBuilder.Entity<Product>(entity =>
    //    {
    //        entity.Property(p => p.Name)
    //            .IsRequired()
    //            .HasMaxLength(200);

    //        entity.Property(p => p.Price)
    //            .HasColumnType("decimal(18,2)");

    //        entity.HasOne(p => p.Category)
    //            .WithMany(c => c.Products)
    //            .HasForeignKey(p => p.CategoryId)
    //            .OnDelete(DeleteBehavior.Restrict);
    //    });

    //    modelBuilder.Entity<Order>(entity =>
    //    {
    //        entity.Property(o => o.TotalAmount)
    //            .HasColumnType("decimal(18,2)");

    //        entity.HasOne(o => o.Customer)
    //            .WithMany(c => c.Orders)
    //            .HasForeignKey(o => o.CustomerId);
    //    });
    //}
}
