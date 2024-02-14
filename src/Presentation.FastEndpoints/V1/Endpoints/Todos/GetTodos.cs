using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using FastEndpoints;
using MediatR;

namespace Presentation.FastEndpoints.V1.Endpoints.Todos;

public sealed class GetTodos(ISender mediator)
    : EndpointWithoutRequest<ICollection<Todo>>
{
    public override void Configure()
    {
        Version(1);
        Get("api/todos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var todos = await mediator.Send(new GetTodosQuery(), cancellationToken);

        await SendOkAsync(todos, cancellationToken);
    }
}