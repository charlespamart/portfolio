using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using CommunityToolkit.Diagnostics;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Persistence.Configuration;

namespace Infrastructure.Persistence;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options), ITodoDbContext
{
    public DbSet<Todo> Todo => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TodoConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

/// <summary>
/// This is necessary to use DbContext in a Function App
/// </summary>
public sealed class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
{
    public TodoDbContext CreateDbContext(string[] args)
    {
        var databaseConnectionString = Environment.GetEnvironmentVariable("Database:Postgres:ConnectionString");

        Guard.IsNotNullOrEmpty(databaseConnectionString);

        var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();

        optionsBuilder.UseNpgsql(databaseConnectionString);

        return new TodoDbContext(optionsBuilder.Options);
    }
}