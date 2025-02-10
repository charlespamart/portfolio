using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.Todos.Commands.CreateTodo;
using Asp.Versioning;
using Carter;
using Carter.ModelBinding;
using Carter.Response;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Common;
using Presentation.V1.Endpoints.Todos.CreateTodo.Request;

namespace Presentation.V1.Endpoints.Todos.CreateTodo;

public class TodoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup(ApiRoutes.Root).WithApiVersionSet(versionSet);

        group.MapPost(ApiRoutes.Todo.CreateTodo, CreateTodoV1)
            .Accepts<CreateTodoRequest>(MediaTypeNames.Application.Json)
            .Produces<Todo>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithName(nameof(CreateTodoV1))
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    private static async Task<IResult> CreateTodoV1(
        HttpContext context,
        [FromBody] CreateTodoRequest createTodoRequest,
        ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await context.Request.ValidateAsync(createTodoRequest);

        if (!result.IsValid)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.Negotiate(result.GetFormattedErrors(), cancellationToken: cancellationToken);
            return Results.BadRequest();
        }

        return Results.Ok("V1");
        var todo = await mediator.Send(createTodoRequest.Adapt<CreateTodoCommand>(), cancellationToken);

        return Results.CreatedAtRoute(nameof(CreateTodo), new { id = todo.Id }, todo);
    }
}