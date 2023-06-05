using FluentValidation;

namespace Presentation.Todos.CreateTodo;

public sealed class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty();
    }
}