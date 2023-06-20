using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    /// <summary>
    /// This is handle by default by EF Core
    /// but it's there to show how it's done
    /// See : https://learn.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations#configuring-a-primary-key
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasIndex(x => x.Id).IsUnique();
    }
}