using System.Collections.Generic;

namespace Todo.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public long? TelegramChatId { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}