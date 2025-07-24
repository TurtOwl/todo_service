using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Application.Common.Interfaces;
public interface ITodoDbContext
{
    DbSet<User> Users { get; }
    DbSet<Project> Projects { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<NotificationJob> NotificationJobs { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}