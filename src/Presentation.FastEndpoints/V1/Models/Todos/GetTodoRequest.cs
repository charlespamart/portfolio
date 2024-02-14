namespace Presentation.FastEndpoints.V1.Models.Todos;

public sealed class GetTodoRequest
{
    public Guid TodoId { get; init; }
}