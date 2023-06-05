using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Extensions;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Presentation.Todos.CreateTodo;
using Presentation.Todos.GetTodo;
using Presentation.Todos.GetTodos;

namespace Presentation.Todos;

public class TodoController
{
    private readonly IMediator _mediator;
    private readonly IValidator<GetTodoRequest> _getTodoValidator;
    private readonly IValidator<CreateTodoRequest> _createTodoValidator;

    public TodoController(IMediator mediator, IValidator<GetTodoRequest> getTodoRequestvalidator,
        IValidator<CreateTodoRequest> createTodoValidator)
    {
        _mediator = mediator;
        _getTodoValidator = getTodoRequestvalidator;
        _createTodoValidator = createTodoValidator;
    }

    [FunctionName(nameof(GetTodo))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "TodoId", In = ParameterLocation.Path, Required = true,
        Type = typeof(string),
        Description = "The Todo's Identifier")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
        bodyType: typeof(Todo),
        Description = $"The OK response a {nameof(Todo)}")]
    public async Task<ActionResult<Todo>> GetTodo(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos/{todoId:guid}")]
        GetTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _getTodoValidator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsNotValid())
            return BadRequest(validationResult.ToReadableFormat());

        var todo = await _mediator.Send(request.ToApplication(), cancellationToken);

        if (todo is null)
            return NotFound();

        return todo;
    }

    [FunctionName(nameof(GetTodos))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "TodoId", In = ParameterLocation.Path, Required = true,
        Type = typeof(string),
        Description = "The Todo's Identifier")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
        bodyType: typeof(IEnumerable<Todo>),
        Description = $"The OK response containing a list of {nameof(Todo)}")]
    public async Task<ActionResult<ICollection<Todo>>> GetTodos(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todos")]
        GetTodosRequest request,
        CancellationToken cancellationToken) =>
        (await _mediator.Send(request.ToApplication(), cancellationToken)).ToList();

    [FunctionName(nameof(CreateTodo))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Todo),
        Description = "The Todo you want to create", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json",
        bodyType: typeof(IEnumerable<Todo>),
        Description = $"The CREATED response containing the {nameof(Todo)} you just created")]
    public async Task<ActionResult<Todo>> CreateTodo(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todos")]
        CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _createTodoValidator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsNotValid())
            return BadRequest(validationResult.ToReadableFormat());

        return await _mediator.Send(request.ToApplication(), cancellationToken);
    }

    private static NotFoundResult NotFound() => new();

    private static BadRequestObjectResult BadRequest(object? obj) => new(obj);
}