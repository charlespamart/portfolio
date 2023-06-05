using FluentValidation;

namespace Application.Handlers.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(model => model.Todo)
            .NotEmpty();
    }
}