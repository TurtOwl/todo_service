using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence;
public class TodoDbContext : DbContext, ITodoDbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> opts) : base(opts) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<NotificationJob> NotificationJobs => Set<NotificationJob>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().HasIndex(u => u.Email).IsUnique();
        b.Entity<TodoItem>().Property(t => t.Status).HasConversion<byte>();
        b.Entity<NotificationJob>().Property(n => n.Channel).HasConversion<byte>();
    }
}