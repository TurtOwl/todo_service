using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Interfaces;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Jobs;
public class NotificationJobs
{
    private readonly IDbContextFactory<TodoDbContext> _factory;
    private readonly IEmailSender _email;
    private readonly ITelegramNotifier _tg;

    public NotificationJobs(IDbContextFactory<TodoDbContext> factory,
        IEmailSender email,
        ITelegramNotifier tg)
    {
        _factory = factory;
        _email = email;
        _tg = tg;
    }

    public async Task SendEmailNotification(int todoId)
    {
        using var ctx = _factory.CreateDbContext();
        var todo = await ctx.TodoItems
            .Include(t => t.Project)
            .ThenInclude(p => p.Owner)
            .FirstOrDefaultAsync(t => t.Id == todoId);
        if (todo is null) return;

        await _email.SendAsync(todo.Project.Owner.Email,
            $"Reminder: {todo.Title}",
            $"Task \"{todo.Title}\" is due soon.");
    }

    public async Task SendTelegramNotification(int todoId)
    {
        using var ctx = _factory.CreateDbContext();
        var todo = await ctx.TodoItems
            .Include(t => t.Project)
            .ThenInclude(p => p.Owner)
            .FirstOrDefaultAsync(t => t.Id == todoId);
        if (todo is null || !todo.Project.Owner.TelegramChatId.HasValue) return;

        await _tg.SendMessage(todo.Project.Owner.TelegramChatId.Value,
            $"⏰ {todo.Title} is due soon!");
    }
}