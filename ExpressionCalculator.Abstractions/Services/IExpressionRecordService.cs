using ExpressionCalculator.Database.Models;

namespace ExpressionCalculator.Abstractions.Services;

public interface IExpressionRecordService
{
    Task<ExpressionRecord> SaveExpression(string expression, decimal result);
    Task<List<ExpressionRecord>> FindByResult(decimal result);
    Task<List<ExpressionRecord>> GetAll();
}