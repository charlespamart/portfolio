using Application.Handlers.Todos.Queries.GetTodo;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Presentation.Common;

namespace Presentation.V2.Endpoints.Todos.GetTodo;

// public sealed class GetTodoEndpoint(ISender sender)
//     : Endpoint<GetTodoQuery, Results<Ok<Todo>, NotFound, ProblemDetails>>
// {
//     public override void Configure()
//     {
//         Version(2);
//         Get(ApiRoutes.Todo.GetTodo);
//         Validator<GetTodoQueryValidator>();
//         Description(setup =>
//         {
//             setup.Produces<Todo>();
//             setup.Produces<ErrorResponse>(StatusCodes.Status400BadRequest);
//             setup.Produces<ErrorResponse>(StatusCodes.Status404NotFound);
//         });
//     }
//
//     public override async Task<Results<Ok<Todo>, NotFound, ProblemDetails>> ExecuteAsync(GetTodoQuery request,
//         CancellationToken cancellationToken)
//     {
//         var todo = await sender.Send(request, cancellationToken);
//
//         if (todo is null)
//             return TypedResults.NotFound();
//         return TypedResults.Ok(todo);
//     }
// }