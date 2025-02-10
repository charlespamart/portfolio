using Application.Common.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandValidator
    : AbstractValidator<CreateTodoCommand>
{
    private readonly ITodoDbContext _todoDbContext;
    private const string IndexAlreadyUsed = $"Can't have the same index for multiple {nameof(Todo)}";

    public CreateTodoCommandValidator(ITodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
        
        RuleFor(model => model.Index)
            .MustAsync(NotHaveTheSameIndex())
            .WithMessage(IndexAlreadyUsed);
    }

    private Func<int, CancellationToken, Task<bool>> NotHaveTheSameIndex() =>
        async (index, cancellationToken) =>
        {
            var todo = await _todoDbContext.Todo.FirstOrDefaultAsync(todo => todo.Index == index, cancellationToken);
            return todo is null;
        };
}