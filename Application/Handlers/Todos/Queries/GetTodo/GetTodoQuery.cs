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
    private readonly ITodoDbContext _gpmDbContext;

    public GetTodoQueryHandler(ITodoDbContext gpmDbContext)
    {
        _gpmDbContext = gpmDbContext;
    }

    public async Task<Todo?> Handle(GetTodoQuery request,
        CancellationToken cancellationToken) =>
        await _gpmDbContext.Todo
            .FindAsync(request.Id, cancellationToken);
}