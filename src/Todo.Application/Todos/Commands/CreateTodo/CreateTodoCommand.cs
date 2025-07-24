using MediatR;

namespace Todo.Application.Todos.Commands.CreateTodo;
public record CreateTodoCommand(
    int ProjectId,
    string Title,
    string? Description,
    DateTime DueDate) : IRequest<int>;