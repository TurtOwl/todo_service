using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Common.Interfaces;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Services;

namespace Todo.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection s, IConfiguration cfg)
    {
        // DbContext
        s.AddDbContext<TodoDbContext>(o =>
            o.UseSqlServer(cfg.GetConnectionString("Sql")));
        s.AddDbContextFactory<TodoDbContext>(lifetime: ServiceLifetime.Transient);

        // Hangfire
        s.AddHangfire(conf => conf
            .UseSqlServerStorage(cfg.GetConnectionString("Hangfire")));
        s.AddHangfireServer();

        // Services
        s.AddScoped<INotificationScheduler, HangfireNotificationScheduler>();
        s.AddTransient<IEmailSender, SmtpEmailSender>();
        s.AddTransient<ITelegramNotifier, TelegramBotNotifier>();
        return s;
    }
}