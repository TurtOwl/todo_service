using Todo.Domain.Enums;

namespace Todo.Application.Common.Dtos;
public record TodoDto(
    int Id,
    string Title,
    string? Description,
    DateTime DueDate,
    TodoStatus Status);