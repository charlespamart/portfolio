namespace Presentation.V1.Endpoints.Todos.GetTodos;

// public sealed class GetTodosEndpoint(ISender sender)
//     : EndpointWithoutRequest<Ok<ICollection<Todo>>>
// {
//     public override void Configure()
//     {
//         Version(1);
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