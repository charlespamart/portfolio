using System.ComponentModel.DataAnnotations;

namespace Domain.OptionConfigurations;

public sealed class InMemoryDbOptions
{
    [Required] public required string DbName { get; init; }
}