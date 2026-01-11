using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpressionCalculator.Database;

public class ExpressionDbContextFactory : IDesignTimeDbContextFactory<ExpressionDbContext>
{
    public ExpressionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExpressionDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=ExpressionCalculator_DB;User ID=SA;Password=St.franciscollege1;MultipleActiveResultSets=False;TrustServerCertificate=True;Connection Timeout=30;");

        return new ExpressionDbContext(optionsBuilder.Options);
    }
}
