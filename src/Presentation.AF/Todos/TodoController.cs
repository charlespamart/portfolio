using System.Net;
using System.Net.Mime;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Presentation.AF.Todos.Models.Todos;
using Microsoft.Azure.Functions.Worker.Http;
using Application.Handlers.Todos.Commands.CreateTodo;
using Application.Handlers.Todos.Queries.GetTodo;
using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using FromBody = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace Presentation.AF.Todos;

public sealed class TodoController(
    ISender mediator)
{
    [Function(nameof(GetTodo))]
    // [OpenApiOperation(operationId: "Run")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    // [OpenApiParameter(name: "TodoId", In = ParameterLocation.Path, Required = true,
    //     Type = typeof(string),
    //     Description = "The Todo's Identifier")]
    // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    //     bodyType: typeof(Todo),
    //     Description = $"The OK response a {nameof(Todo)}")]
    public async Task<IActionResult> GetTodo(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos/{todoId:guid}")] [FromQuery]
        Guid todoId,
        CancellationToken cancellationToken)
    {
        var todo = await mediator.Send(new GetTodoQuery { Id = todoId }, cancellationToken);

        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [Function(nameof(GetTodos))]
    // [OpenApiOperation(operationId: "Run")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    // [OpenApiParameter(name: "TodoId", In = ParameterLocation.Path, Required = true,
    //     Type = typeof(string),
    //     Description = "The Todo's Identifier")]
    // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    //     bodyType: typeof(IEnumerable<Todo>),
    //     Description = $"The OK response containing a list of {nameof(Todo)}")]
    public async Task<IActionResult> GetTodos(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos")]
        HttpRequestData request,
        CancellationToken cancellationToken) =>
        Ok((await mediator.Send(new GetTodosQuery(), cancellationToken)).ToList());

    [Function(nameof(CreateTodo))]
    // [OpenApiOperation(operationId: "Run")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    // [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Todo),
    //     Description = "The Todo you want to create", Required = true)]
    // [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json",
    //     bodyType: typeof(IEnumerable<Todo>),
    //     Description = $"The CREATED response containing the {nameof(Todo)} you just created")]
    public async Task<HttpResponseData> CreateTodo(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos")]
        HttpRequestData request,
        CancellationToken cancellationToken)
    {
        var smt = request.ReadFromJsonAsync<CreateTodoRequest>(cancellationToken);
        var something = request.CreateResponse(HttpStatusCode.Created);
        something.Headers.Add("Content-Type", MediaTypeNames.Application.Json);
        var todo = await mediator.Send(smt.Adapt<CreateTodoCommand>(), cancellationToken);
        await something.WriteAsJsonAsync(todo, cancellationToken: cancellationToken);
        return something;
    }

    private static NotFoundResult NotFound() => new();
    private static OkObjectResult Ok(object? value) => new(value);

    private static CreatedResult Created(string location, object? value) =>
        new(location, value);
}