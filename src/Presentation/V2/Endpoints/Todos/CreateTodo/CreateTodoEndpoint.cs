using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.Todos.Commands.CreateTodo;
using Asp.Versioning;
using Carter;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Common;
using Presentation.V2.Endpoints.Todos.CreateTodo.Request;

namespace Presentation.V2.Endpoints.Todos.CreateTodo;

public class TodoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup(ApiRoutes.Root);

        group.MapPost(ApiRoutes.Todo.CreateTodo, CreateTodoV2)
            .Accepts<CreateTodoRequest>(MediaTypeNames.Application.Json)
            .Produces<Todo>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithName(nameof(CreateTodoV2))
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(2)
            .WithOpenApi();
    }

    private static async Task<IResult> CreateTodoV2(
        [FromBody] CreateTodoRequest createTodoRequest,
        ISender mediator,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(createTodoRequest.Adapt<CreateTodoCommand>(), cancellationToken);
        
        return Results.CreatedAtRoute(nameof(CreateTodo), new { id = todo.Id }, todo);
    }
}