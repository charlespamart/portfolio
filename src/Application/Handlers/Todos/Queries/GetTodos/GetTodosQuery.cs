using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Todos.Queries.GetTodos;

public sealed record GetTodosQuery : IRequest<ICollection<Todo>>;

public sealed class
    GetTodosQueryHandler(ITodoDbContext gpmDbContext) : IRequestHandler<GetTodosQuery,
    ICollection<Todo>>
{
    public async Task<ICollection<Todo>> Handle(GetTodosQuery request,
        CancellationToken cancellationToken) =>
        await gpmDbContext.Todo
            .AsNoTracking().ToListAsync(cancellationToken);
}