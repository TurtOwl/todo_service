using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Todos.Commands.CreateTodo;
using Todo.Application.Todos.Commands.UpdateTodo;
using Todo.Application.Todos.Queries.GetAllTodos;

namespace Todo.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ISender _sender;
    public TodosController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int projectId)
    {
        var result = await _sender.Send(new GetAllTodosQuery(projectId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoCommand cmd)
    {
        var id = await _sender.Send(cmd);
        return CreatedAtAction(nameof(GetAll), new { projectId = cmd.ProjectId }, new { id });
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTodoCommand cmd)
    {
        var fullCmd = cmd with { Id = id };
        await _sender.Send(fullCmd);
        return NoContent();
    }
}