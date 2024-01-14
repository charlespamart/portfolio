using System.ComponentModel.DataAnnotations;

namespace Domain.OptionConfigurations;

public sealed class PostgresDbOptions
{
    [Required] public required string ConnectionString { get; init; }
}