using FluentValidation;
using Todo.Application.Todos.Commands.CreateTodo;

namespace Todo.Application.Todos.Commands.CreateTodo;
public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
        RuleFor(x => x.DueDate).GreaterThan(DateTime.UtcNow);
    }
}