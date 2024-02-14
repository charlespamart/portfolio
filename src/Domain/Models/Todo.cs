namespace Domain.Models;

public sealed record Todo
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}