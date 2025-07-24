using Todo.Domain.Enums;
using System;

namespace Todo.Domain.Entities;
public class TodoItem
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}