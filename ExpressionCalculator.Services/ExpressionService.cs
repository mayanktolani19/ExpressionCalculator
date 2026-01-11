using ExpressionCalculator.Abstractions.Services;
using ExpressionCalculator.Services.Validation;
using Microsoft.Extensions.Logging;

namespace ExpressionCalculator.Services;

public class ExpressionService(ExpressionValidator expressionValidator, ILogger<ExpressionService> logger) : IExpressionService
{
    public decimal CalculateExpression(string expression)
    {
            var validationResult = expressionValidator.Validate(expression);
            if (!validationResult.IsValid)
            {
                var message = string.Join(';', validationResult.Errors.Select(x => x.ErrorMessage));
                logger.LogError("Expression validation failed: {Message}", message);
                throw new ArgumentException($"Invalid expression: " + message);
            }
                
            var result = EvaluateExpression(expression);
            logger.LogInformation("Expression '{Expression}' evaluated to {Result}", expression, result);
            return Math.Round(result, 2);
    }

    private decimal EvaluateExpression(string expression)
    {
        var stack = new Stack<decimal>();
        decimal num = 0;
        var prevOp = '+';

        for (var i = 0; i < expression.Length; i++)
        {
            var c = expression[i];

            if (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
            }

            if ((!char.IsDigit(c) && c != ' ') || i == expression.Length - 1)
            {
                switch (prevOp)
                {
                    case '+':
                        stack.Push(num);
                        break;

                    case '-':
                        stack.Push(-num);
                        break;

                    case '*':
                        stack.Push(stack.Pop() * num);
                        break;

                    case '/':
                        if (num == 0)
                        {
                            logger.LogError("Division by zero encountered in expression evaluation.");
                            throw new DivideByZeroException("Invalid Expression: Division by zero in expression");
                        }

                        stack.Push(stack.Pop() / num);
                        break;
                }

                prevOp = c;
                num = 0;
            }
        }

        decimal result = 0;
        while (stack.Count > 0)
            result += stack.Pop();

        return result;
    }
}