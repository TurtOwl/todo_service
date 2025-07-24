using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Todo.Infrastructure;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddInfrastructure(ctx.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Hangfire уже запущен через Infrastructure
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}