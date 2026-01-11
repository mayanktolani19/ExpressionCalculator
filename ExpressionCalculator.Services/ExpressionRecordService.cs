using ExpressionCalculator.Abstractions.Services;
using ExpressionCalculator.Database;
using ExpressionCalculator.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpressionCalculator.Services;

public class ExpressionRecordService(ExpressionDbContext context, ILogger<ExpressionRecordService>logger) : IExpressionRecordService
{
    public async Task<ExpressionRecord> SaveExpression(string expression, decimal result)
    {
        logger.LogInformation($"Saving expression in DB {expression}");
        var record = new ExpressionRecord
        {
            Expression = expression,
            Result = result,
            CreatedAt = DateTime.UtcNow
        };

        context.ExpressionRecords.Add(record);
        await context.SaveChangesAsync();
        logger.LogInformation("Expression saved with ID {Id}", record.Id);
        
        return record;
    }

    public async Task<List<ExpressionRecord>> FindByResult(decimal result)
    {
        return await context.ExpressionRecords
            .Where(e => e.Result == result)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ExpressionRecord>> GetAll()
    {
        return await context.ExpressionRecords
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }
}