using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Dtos;
using Todo.Application.Common.Interfaces;

namespace Todo.Application.Todos.Queries.GetAllTodos;
public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, IReadOnlyList<TodoDto>>
{
    private readonly ITodoDbContext _ctx;
    public GetAllTodosQueryHandler(ITodoDbContext ctx) => _ctx = ctx;

    public async Task<IReadOnlyList<TodoDto>> Handle(GetAllTodosQuery req, CancellationToken ct)
    {
        return await _ctx.TodoItems
            .Where(t => t.ProjectId == req.ProjectId)
            .OrderByDescending(t => t.CreatedDate)
            .Select(t => new TodoDto(
                t.Id,
                t.Title,
                t.Description,
                t.DueDate,
                t.Status))
            .ToListAsync(ct);
    }
}