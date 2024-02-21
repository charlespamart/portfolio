using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using FastEndpoints;
using MediatR;
using Presentation.FastEndpoints.Common;

namespace Presentation.FastEndpoints.V1.Endpoints.Todos.GetTodos;

public sealed class GetTodosEndpoint(ISender mediator)
    : EndpointWithoutRequest<ICollection<Todo>>
{
    public override void Configure()
    {
        Version(1);
        Get(ApiRoutes.Todo.GetTodos);
        Description(setup =>
        {
            setup.Produces<ICollection<Todo>>();
        });
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var todos = await mediator.Send(new GetTodosQuery(), cancellationToken);

        await SendOkAsync(todos, cancellationToken);
    }
}