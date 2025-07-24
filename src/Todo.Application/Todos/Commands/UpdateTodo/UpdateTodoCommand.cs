using MediatR;
using Todo.Domain.Enums;

namespace Todo.Application.Todos.Commands.UpdateTodo;
public record UpdateTodoCommand(
    int Id,
    string? Title,
    string? Description,
    DateTime? DueDate,
    TodoStatus? Status) : IRequest;