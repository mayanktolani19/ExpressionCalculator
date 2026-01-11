using ExpressionCalculator.Services.Validation;
using FluentAssertions;

namespace ExpressionCalculator.Tests;

public class ExpressionValidatorTests
{
    private readonly ExpressionValidator _validator = new();

    [Theory]
    [InlineData("3+4*6-12")]
    [InlineData("10 + 5 / 2")]
    [InlineData("100/25 + 6*3")]
    [InlineData("1")]
    [InlineData("  10   +   20  *  3  ")]
    public void Validate_Should_Pass_ForValidExpressions(string expression)
    {
        // Act
        var result = _validator.Validate(expression);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_Should_Fail_WhenExpressionIsEmpty()
    {
        var result = _validator.Validate("");

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e =>
            e.ErrorMessage == "Expression cannot be empty.");
    }

    [Theory]
    [InlineData("3+a")]
    [InlineData("10%2")]
    [InlineData("5&6")]
    public void Validate_Should_Fail_ForInvalidCharacters(string expression)
    {
        var result = _validator.Validate(expression);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.ErrorMessage == "Expression contains invalid characters.");
    }
    
    [Theory]
    [InlineData("+3+4")]
    [InlineData("*10+2")]
    [InlineData("10+")]
    [InlineData("5/")]
    public void Validate_Should_Fail_WhenExpressionStartsOrEndsWithOperator(string expression)
    {
        var result = _validator.Validate(expression);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.ErrorMessage == "Expression cannot start or end with an operator.");
    }

    [Theory]
    [InlineData("3++4")]
    [InlineData("10--2")]
    [InlineData("5**6")]
    [InlineData("8//4")]
    [InlineData("10 + * 2")]
    public void Validate_Should_Fail_ForConsecutiveOperators(string expression)
    {
        var result = _validator.Validate(expression);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.ErrorMessage == "Expression cannot contain consecutive operators.");
    }

    [Fact]
    public void Validate_Should_ReturnMultipleErrors_WhenApplicable()
    {
        var result = _validator.Validate("+10++2a");

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThan(1);
    }
}
