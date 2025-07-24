namespace Todo.Application.Common.Interfaces;
public interface INotificationScheduler
{
    Task ScheduleAsync(int todoItemId, DateTime dueDate);
    Task RescheduleAsync(int todoItemId, DateTime newDueDate);
}