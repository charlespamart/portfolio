namespace Presentation.FastEndpoints.V1.Models.Todos;

public sealed record CreateTodoRequest
{
    public required string Name { get; init; }
}