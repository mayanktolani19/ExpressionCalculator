using ExpressionCalculator.Abstractions.Services;
using ExpressionCalculator.API;
using ExpressionCalculator.Database;
using ExpressionCalculator.Services;
using ExpressionCalculator.Services.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ExpressionDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IExpressionService, ExpressionService>();
builder.Services.AddScoped<IExpressionRecordService, ExpressionRecordService>();
builder.Services.AddScoped<ExpressionValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapExpressionEndpoints();

app.Run();