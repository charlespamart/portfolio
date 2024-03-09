using System.Net;
using Application.Handlers.Todos.Queries.GetTodo;
using Domain.Models;
using FastEndpoints;
using MediatR;
using Presentation.FastEndpoints.Common;
using Presentation.FastEndpoints.V2.Endpoints.Todos.GetTodo.Models;

namespace Presentation.FastEndpoints.V2.Endpoints.Todos.GetTodo;

public sealed class GetTodoEndpoint(ISender mediator)
    : Endpoint<GetTodoRequest, Todo>
{
    public override void Configure()
    {
        Version(2);
        Get(ApiRoutes.Todo.GetTodo);
        Description(setup =>
        {
            setup.Produces<Todo>();
            setup.Produces<ErrorResponse>((int)HttpStatusCode.BadRequest);
            setup.Produces<ErrorResponse>((int)HttpStatusCode.NotFound);
        });
    }

    public override async Task HandleAsync(GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = request.TodoId }, cancellationToken);

        if (todo is null)
            await SendNotFoundAsync(cancellationToken);
        else
            await SendOkAsync(todo!, cancellationToken);
    }
}