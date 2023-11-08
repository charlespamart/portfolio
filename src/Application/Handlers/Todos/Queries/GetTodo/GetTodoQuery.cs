using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Todos.Queries.GetTodo;

public sealed record GetTodoQuery : IRequest<Todo?>
{
    public Guid Id { get; init; }
}

public sealed record
    GetTodoQueryHandler : IRequestHandler<GetTodoQuery, Todo?>
{
    private readonly ITodoDbContext _todoDbContext;

    public GetTodoQueryHandler(ITodoDbContext gpmDbContext)
    {
        _todoDbContext = gpmDbContext;
    }

    public async Task<Todo?> Handle(GetTodoQuery request,
        CancellationToken cancellationToken) =>
        await _todoDbContext.Todo
            .FindAsync(request.Id, cancellationToken);
}