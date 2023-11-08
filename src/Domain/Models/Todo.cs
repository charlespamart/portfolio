namespace Domain.Models;

public sealed record Todo
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}