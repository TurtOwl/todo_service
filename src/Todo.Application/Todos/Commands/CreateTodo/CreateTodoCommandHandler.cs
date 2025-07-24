using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Todos.Commands.CreateTodo;
public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
    private readonly ITodoDbContext _ctx;
    private readonly INotificationScheduler _scheduler;
    public CreateTodoCommandHandler(ITodoDbContext ctx, INotificationScheduler scheduler)
    {
        _ctx = ctx;
        _scheduler = scheduler;
    }

    public async Task<int> Handle(CreateTodoCommand req, CancellationToken ct)
    {
        var todo = new TodoItem
        {
            ProjectId = req.ProjectId,
            Title = req.Title,
            Description = req.Description,
            DueDate = req.DueDate,
            Status = Domain.Enums.TodoStatus.New
        };
        _ctx.TodoItems.Add(todo);
        await _ctx.SaveChangesAsync(ct);

        await _scheduler.ScheduleAsync(todo.Id, todo.DueDate);

        return todo.Id;
    }
}