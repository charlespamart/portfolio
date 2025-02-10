using Application.Handlers.Todos.Queries.GetTodos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Presentation.Common;

namespace Presentation.V2.Endpoints.Todos.GetTodos;

// public sealed class GetTodosEndpoint(ISender sender)
//     : EndpointWithoutRequest<Ok<ICollection<Todo>>>
// {
//     public override void Configure()
//     {
//         Version(2);
//         Get(ApiRoutes.Todo.GetTodos);
//         Description(setup => { setup.Produces<ICollection<Todo>>(); });
//     }
//
//     public override async Task<Ok<ICollection<Todo>>> HandleAsync(CancellationToken cancellationToken)
//     {
//         var todos = await sender.Send(new GetTodosQuery(), cancellationToken);
//
//         return TypedResults.Ok(todos);
//     }
// }