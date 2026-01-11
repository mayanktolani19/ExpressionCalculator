using Microsoft.EntityFrameworkCore;
using ExpressionCalculator.Database.Models;

namespace ExpressionCalculator.Database;

public class ExpressionDbContext : DbContext
{
    public ExpressionDbContext(DbContextOptions<ExpressionDbContext> options) : base(options)
    {
    }

    public DbSet<ExpressionRecord> ExpressionRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExpressionRecord>()
            .HasIndex(e => e.Result);
    }
}