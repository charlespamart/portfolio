using Application.Common.Interfaces;
using CommunityToolkit.Diagnostics;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    private const string DatabaseName = "TodoDB";

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) =>
        services.AddPostgresTodoDbContext();

    private static IServiceCollection AddPostgresTodoDbContext(this IServiceCollection services)
    {
        var postgresDatabaseConnectionString =
            Environment.GetEnvironmentVariable("Databases:Postgres:ConnectionString");

        Guard.IsNotNullOrEmpty(postgresDatabaseConnectionString);

        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(postgresDatabaseConnectionString));

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static IServiceCollection AddMySqlTodoDbContext(this IServiceCollection services)
    {
        var mySqlDatabaseConnectionString = Environment.GetEnvironmentVariable("Databases:MySQL:ConnectionString");

        Guard.IsNotNullOrEmpty(mySqlDatabaseConnectionString);

        services.AddDbContext<TodoDbContext>(options => options.UseMySQL(mySqlDatabaseConnectionString));

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static IServiceCollection AddInMemoryTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase(DatabaseName));

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }
}