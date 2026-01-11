using System.Text.RegularExpressions;
using FluentValidation;

namespace ExpressionCalculator.Services.Validation;

public partial class ExpressionValidator : AbstractValidator<string>
{
    public ExpressionValidator()
    {
        RuleFor(exp => exp)
            .NotEmpty()
            .WithMessage("Expression cannot be empty.")

            .Must(ContainValidCharacters)
            .WithMessage("Expression contains invalid characters.")

            .Must(NotStartOrEndWithOperator)
            .WithMessage("Expression cannot start or end with an operator.")

            .Must(NotContainConsecutiveOperators)
            .WithMessage("Expression cannot contain consecutive operators.");
    }

    private static bool ContainValidCharacters(string exp)
    {
        return ValidCharactersRegex().IsMatch(exp);
    }

    private static bool NotStartOrEndWithOperator(string exp)
    {
        exp = exp.Trim();
        return exp.Length > 0 &&
               !IsOperator(exp[0]) &&
               !IsOperator(exp[^1]);
    }

    private static bool NotContainConsecutiveOperators(string exp)
    {
        exp = NotContainConsecutiveOperatorsRegex().Replace(exp, "");
        for (var i = 1; i < exp.Length; i++)
        {
            if (IsOperator(exp[i]) && IsOperator(exp[i - 1]))
                return false;
        }
        return true;
    }
    
    private static bool IsOperator(char c)
    {
        return c is '+' or '-' or '*' or '/';
    }

    [GeneratedRegex(@"^[0-9+\-*/\s]+$")]
    private static partial Regex ValidCharactersRegex();
    
    [GeneratedRegex(@"\s+")]
    private static partial Regex NotContainConsecutiveOperatorsRegex();
}