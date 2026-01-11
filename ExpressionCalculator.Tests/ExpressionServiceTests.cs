using ExpressionCalculator.Services;
using ExpressionCalculator.Services.Validation;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace ExpressionCalculator.Tests;

public class ExpressionServiceTests
{
    private readonly ExpressionService _sut;

    public ExpressionServiceTests()
    {
        var validator = new ExpressionValidator();
        var logger = new Logger<ExpressionService>(new LoggerFactory());
        _sut = new ExpressionService(validator, logger);
    }

    [Theory]
    [InlineData("3+4*6-12", 15)]
    [InlineData("10+5/2", 12.5)]
    [InlineData("8/4+1", 3)]
    [InlineData("100/25+6*3", 22)]
    public void CalculateExpression_Should_ReturnCorrectResult(string expression, decimal expected)
    {
        var result = _sut.CalculateExpression(expression);

        result.Should().Be(expected);
    }

    [Fact]
    public void CalculateExpression_Should_HandleSpaces()
    {
        var result = _sut.CalculateExpression(" 10 +  5 / 2 ");

        result.Should().Be(12.5m);
    }

    [Fact]
    public void CalculateExpression_Should_RoundToTwoDecimals()
    {
        var result = _sut.CalculateExpression("10/3");

        result.Should().Be(3.33m);
    }

    [Theory]
    [InlineData("5/0")]
    [InlineData("10 + 0 / 0")]
    public void CalculateExpression_Should_ThrowDivideByZeroException(string expression)
    {
        Action act = () => _sut.CalculateExpression(expression);

        act.Should()
           .Throw<DivideByZeroException>()
           .WithMessage("Invalid Expression: Division by zero in expression");
    }

    [Theory]
    [InlineData("")]
    [InlineData("++")]
    [InlineData("10+")]
    [InlineData("*5+2")]
    [InlineData("10//2")]
    public void CalculateExpression_Should_ThrowArgumentException_ForInvalidExpression(string expression)
    {
        Action act = () => _sut.CalculateExpression(expression);

        act.Should()
           .Throw<ArgumentException>()
           .WithMessage("Invalid expression:*");
    }
}