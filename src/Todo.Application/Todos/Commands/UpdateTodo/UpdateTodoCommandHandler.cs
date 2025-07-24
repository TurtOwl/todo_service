using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Interfaces;
using Todo.Domain.Events;

namespace Todo.Application.Todos.Commands.UpdateTodo;
public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
{
    private readonly ITodoDbContext _ctx;
    private readonly INotificationScheduler _scheduler;
    private readonly IPublisher _publisher;
    public UpdateTodoCommandHandler(ITodoDbContext ctx, INotificationScheduler scheduler, IPublisher publisher)
    {
        _ctx = ctx;
        _scheduler = scheduler;
        _publisher = publisher;
    }

    public async Task Handle(UpdateTodoCommand req, CancellationToken ct)
    {
        var todo = await _ctx.TodoItems.FindAsync(new object[] { req.Id }, ct);
        if (todo is null) throw new KeyNotFoundException();

        if (req.Title is not null) todo.Title = req.Title;
        if (req.Description is not null) todo.Description = req.Description;
        if (req.DueDate.HasValue)
        {
            todo.DueDate = req.DueDate.Value;
            await _scheduler.RescheduleAsync(todo.Id, todo.DueDate);
        }
        if (req.Status.HasValue) todo.Status = req.Status.Value;

        todo.ModifiedDate = DateTime.UtcNow;
        await _ctx.SaveChangesAsync(ct);

        if (todo.DueDate.Subtract(DateTime.UtcNow).TotalHours <= 24)
            await _publisher.Publish(new TodoItemDueSoonEvent(todo), ct);
    }
}