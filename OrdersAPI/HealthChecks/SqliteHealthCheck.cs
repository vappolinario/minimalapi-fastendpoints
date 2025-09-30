using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OrdersAPI.HealthChecks;

public class SqliteHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;

    public SqliteHealthCheck(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();

            bool tableExists = _context.Database
                                 .SqlQuery<int?>($"SELECT Count(name) as Value FROM sqlite_master WHERE type='table' AND name='Orders'")
                                 .SingleOrDefault() != null;


            if (!tableExists)
            {
                return new HealthCheckResult(
                    context.Registration.FailureStatus,
                    "Orders table not found in the database.");
            }

            return HealthCheckResult.Healthy("SQLite database is healthy and contains the Orders table.");
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(
                context.Registration.FailureStatus,
                "Failed to connect to SQLite database or query Orders table.",
                ex);
        }
    }
}

