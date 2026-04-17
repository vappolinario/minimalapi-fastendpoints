using FastEndpoints;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OrdersAPI.HealthChecks;
using OrdersAPI.Data;
using OrdersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(x =>
    x.AddOtlpExporter(e =>
      {
        e.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
        e.Protocol = OtlpExportProtocol.HttpProtobuf;
        e.Headers = "X-Seq-APiKey=mIoncoNqt0KPOCIKgDtG";
        })
    );

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck<SqliteHealthCheck>(
        "sqlite_check",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: ["database", "sqlite"]);

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
