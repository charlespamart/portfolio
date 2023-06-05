using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand : IRequest<Todo>
{
    public Todo Todo { get; init; } = null!;
}

public sealed record
    CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo>
{
    private readonly ITodoDbContext _todoDbContext;

    public CreateTodoCommandHandler(ITodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task<Todo> Handle(CreateTodoCommand request,
        CancellationToken cancellationToken)
    {
        _todoDbContext.Todo.Add(request.Todo);

        await _todoDbContext.SaveChangesAsync(cancellationToken);

        return request.Todo;
    }
}