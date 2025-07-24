using System.Collections.Generic;

namespace Todo.Domain.Entities;
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int OwnerId { get; set; }
    public User Owner { get; set; } = default!;

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}