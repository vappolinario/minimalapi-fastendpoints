using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck<SqliteHealthCheck>(
        "sqlite_check",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "database", "sqlite" });

builder.Services.AddFastEndpoints();
var app = builder.Build();

app.UseFastEndpoints();

// Map health check endpoint
app.MapHealthChecks("/health/ready", new HealthCheckOptions()
{
    Predicate = (check) => check.Tags.Contains("database"),
});

app.MapHealthChecks("/health/live", new HealthCheckOptions()
{
    Predicate = (_) => false
});

app.Run();
