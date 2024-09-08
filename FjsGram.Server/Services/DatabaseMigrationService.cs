using FjsGram.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FjsGram.Server.Services;

public class DatabaseMigrationService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private static readonly ActivitySource _activitySource = new("DatabaseMigration");
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = _activitySource.StartActivity();
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<FjsGramContext>();
        await context.Database.MigrateAsync(stoppingToken);
    }
}
