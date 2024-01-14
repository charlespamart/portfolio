using System.ComponentModel.DataAnnotations;

namespace Domain.OptionConfigurations;

public sealed class MySqlDbOptions
{
    [Required] public required string ConnectionString { get; init; }
}