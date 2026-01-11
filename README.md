# 🧮 Expression Calculator API

A .NET Web API that validates and evaluates mathematical expressions containing integers and operators (`+ - * /`), applying correct operator precedence and returning decimal results.

---

## ✨ Features

- Expression validation using FluentValidation
- Arithmetic evaluation with operator precedence
- Supports spaces in expressions
- Decimal results for division
- Division-by-zero detection
- Structured logging using ILogger
- Persistence using Entity Framework Core
- Swagger UI for API exploration
- Unit tests with xUnit and FluentAssertions

---

## ⚙️ Prerequisites

- .NET SDK 10.0+
- SQL Server / SQL Server Express / LocalDB
- EF Core CLI tools (Run the below command if not installed)
```bash
dotnet tool install --global dotnet-ef
```

## Setup
Clone and cd into the repo:
```
git clone https://github.com/mayanktolani19/ExpressionCalculator.git
cd ExpressionCalculator
```

### Run database migration
```
cd ExpressionCalculator.Database
dotnet ef migrations add InitialCreate
dotnet ef database update
```

The ExpressionCalculator.API must be set as the starter project.

### The API will be available at
```
http://localhost:5032/swagger/index.html
```
