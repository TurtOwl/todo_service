using MediatR;

namespace Todo.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string Email,
    string Password) : IRequest<int>;