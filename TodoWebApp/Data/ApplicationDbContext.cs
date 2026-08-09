using Microsoft.EntityFrameworkCore;
using TodoWebApp.Models;

namespace TodoWebApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //// DbSets for your entities
    public DbSet<TodoItem> TodoItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //modelBuilder.Entity<TodoItem>(entity =>
        //{
        //    entity.Property(e => e.CreatedAt)
        //        .HasDefaultValueSql("CURRENT_TIMESTAMP");
        //});
    }
}
