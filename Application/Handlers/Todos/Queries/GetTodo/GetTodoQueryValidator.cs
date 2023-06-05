using FluentValidation;

namespace Application.Handlers.Todos.Queries.GetTodo;

public sealed class GetTodoQueryValidator : AbstractValidator<GetTodoQuery>
{
    public GetTodoQueryValidator()
    {
        RuleFor(model => model.Id).NotEmpty();
    }
}