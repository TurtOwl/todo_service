using Todo.Domain.Enums;
using System;

namespace Todo.Domain.Entities;
public class NotificationJob
{
    public int Id { get; set; }
    public int TodoItemId { get; set; }
    public TodoItem TodoItem { get; set; } = default!;
    public DateTime NotifyAt { get; set; }
    public NotificationChannel Channel { get; set; }
    public bool Sent { get; set; }
}