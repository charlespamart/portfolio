using Application.Handlers.Todos.Queries.GetTodo;
using Domain.Models;
using FastEndpoints;
using MediatR;
using Presentation.FastEndpoints.V2.Endpoints.Todos.GetTodo.Models;

namespace Presentation.FastEndpoints.V2.Endpoints.Todos.GetTodo;

public sealed class GetTodoEndpoint(ISender mediator)
    : Endpoint<GetTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(2);
        Get("api/todos/{TodoId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = request.TodoId }, cancellationToken);

        if (todo is null)
            await SendNotFoundAsync(cancellationToken);
        await SendOkAsync(todo!, cancellationToken);
    }
}