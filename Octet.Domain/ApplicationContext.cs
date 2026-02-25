using Microsoft.EntityFrameworkCore;
using Octet.Domain;

namespace Octet.Domain;

public class ApplicationContext : DbContext
{
    /// <summary>
    /// Таблица с результатами
    /// </summary>
    public DbSet<Result> Results { get; set; }
    
    /// <summary>
    /// Таблица с данными
    /// </summary>
    public DbSet<Value> Values { get; set; }

    /// <summary>
    /// Конструктор контекста
    /// </summary>
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options) 
    { 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Result>().ToTable("results");
        modelBuilder.Entity<Value>().ToTable("file_values");
        
        modelBuilder.Entity<Result>().HasKey(x => x.Id);
        modelBuilder.Entity<Value>().HasKey(x => x.Id);
        
        modelBuilder.Entity<Result>().HasIndex(x => x.FileName).IsUnique();

        modelBuilder.Entity<Value>().HasOne(x => x.Result)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.ResultId)
            .OnDelete(DeleteBehavior.Cascade);;
    }
}