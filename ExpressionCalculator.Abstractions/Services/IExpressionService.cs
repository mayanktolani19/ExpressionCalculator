namespace ExpressionCalculator.Abstractions.Services;

public interface IExpressionService
{
    public decimal CalculateExpression(string expression);
}