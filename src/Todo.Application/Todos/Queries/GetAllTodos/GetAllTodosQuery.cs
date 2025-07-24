using MediatR;
using Todo.Application.Common.Dtos;

namespace Todo.Application.Todos.Queries.GetAllTodos;
public record GetAllTodosQuery(int ProjectId) : IRequest<IReadOnlyList<TodoDto>>;