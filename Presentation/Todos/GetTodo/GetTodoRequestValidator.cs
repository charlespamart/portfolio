using FluentValidation;

namespace Presentation.Todos.GetTodo;

public sealed class GetTodoRequestValidator : AbstractValidator<GetTodoRequest>
{
    public GetTodoRequestValidator()
    {
        RuleFor(model => model.Id)
            .NotEmpty();
    }
}