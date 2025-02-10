using FluentValidation;

namespace Presentation.V1.Endpoints.Todos.CreateTodo.Request;

public sealed class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty();
        RuleFor(model => model.Index)
            .NotEmpty();
    }
}