using System;
using System.Threading.Tasks;
using Hangfire;
using Todo.Application.Common.Interfaces;
using Todo.Infrastructure.Jobs;   

namespace Todo.Infrastructure.Services;

public class HangfireNotificationScheduler : INotificationScheduler
{
    public Task ScheduleAsync(int todoItemId, DateTime dueDate)
    {
        BackgroundJob.Schedule<NotificationJobs>(j => j.SendEmailNotification(todoItemId),
            dueDate.AddDays(-1));
        BackgroundJob.Schedule<NotificationJobs>(j => j.SendTelegramNotification(todoItemId),
            dueDate.AddHours(-1));
        return Task.CompletedTask;
    }

    public Task RescheduleAsync(int todoItemId, DateTime newDueDate)
    {
        return ScheduleAsync(todoItemId, newDueDate);
    }
}