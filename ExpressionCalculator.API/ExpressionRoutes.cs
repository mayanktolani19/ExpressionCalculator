using ExpressionCalculator.Abstractions.DTOs;
using ExpressionCalculator.Abstractions.Services;

namespace ExpressionCalculator.API;

public static class ExpressionRoutes
{
    public static void MapExpressionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/expression")
            .WithName("Expression");

        group.MapPost("/calculate", Calculate)
            .WithName("Calculate Expression")
            .WithDescription("Calculate a mathematical expression");

        group.MapGet("/by-result/{result}", GetByResult)
            .WithName("Get By Result")
            .WithDescription("Find expressions by their result");

        group.MapGet("/all", GetAll)
            .WithName("Get All Expressions")
            .WithDescription("Get all expression records");
    }

    private static async Task<IResult> Calculate(
        ExpressionRequest request,
        IExpressionService expressionService,
        IExpressionRecordService recordService)
    {
        if (string.IsNullOrWhiteSpace(request.Expression))
            return Results.BadRequest("Expression cannot be empty");

        try
        {
            var result = expressionService.CalculateExpression(request.Expression);
            var record = await recordService.SaveExpression(request.Expression, result);
            return Results.Ok(new { record.Id, record.Expression, record.Result, record.CreatedAt });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetByResult(
        decimal result,
        IExpressionRecordService recordService)
    {
        var records = await recordService.FindByResult(result);
        return Results.Ok(records);
    }

    private static async Task<IResult> GetAll(IExpressionRecordService recordService)
    {
        var records = await recordService.GetAll();
        return Results.Ok(records);
    }
}
