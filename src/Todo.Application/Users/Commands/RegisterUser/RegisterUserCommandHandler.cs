using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Common.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly ITodoDbContext _ctx;
    public RegisterUserCommandHandler(ITodoDbContext ctx) => _ctx = ctx;

    public async Task<int> Handle(RegisterUserCommand req, CancellationToken ct)
    {
        if (await _ctx.Users.AnyAsync(u => u.Email == req.Email, ct))
            throw new InvalidOperationException("Email already exists");

        var user = new User
        {
            Email = req.Email.ToLowerInvariant(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password)
        };
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync(ct);
        return user.Id;
    }
}