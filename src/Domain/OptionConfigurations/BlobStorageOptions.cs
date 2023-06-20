namespace Domain.OptionConfigurations;

public sealed record BlobStorageOptions
{
    public string ConnectionString { get; init; } = null!;
    public string FolderName { get; set; } = null!;
}