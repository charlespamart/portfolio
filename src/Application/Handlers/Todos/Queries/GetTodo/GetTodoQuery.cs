using Application.Common.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Todos.Queries.GetTodo;

public sealed record GetTodoQuery : IRequest<Todo?>
{
    public Guid Id { get; init; }
}

public sealed class
    GetTodoQueryHandler(ITodoDbContext todoDbContext) : IRequestHandler<GetTodoQuery, Todo?>
{
    public async Task<Todo?> Handle(GetTodoQuery request,
        CancellationToken cancellationToken) =>
        await todoDbContext.Todo
            .FindAsync([request.Id], cancellationToken: cancellationToken);
}