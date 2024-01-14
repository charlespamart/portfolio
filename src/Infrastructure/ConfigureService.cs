using Application.Common.Interfaces;
using Domain.OptionConfigurations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) =>
        services.AddInMemoryTodoDbContext();

    private static IServiceCollection AddPostgresTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var postgresDbOptions = serviceProvider.GetRequiredService<IOptions<PostgresDbOptions>>();
            var postgresDbConnectionString = postgresDbOptions.Value.ConnectionString;
            options.UseNpgsql(postgresDbConnectionString);
        });

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static IServiceCollection AddMySqlTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var mySqlDbOptions = serviceProvider.GetRequiredService<IOptions<MySqlDbOptions>>();
            var mySqlDbConnectionString = mySqlDbOptions.Value.ConnectionString;
            options.UseMySQL(mySqlDbConnectionString);
        });

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static IServiceCollection AddInMemoryTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var inMemoryDbOptions = serviceProvider.GetRequiredService<IOptions<InMemoryDbOptions>>();
            var inMemoryDbName = inMemoryDbOptions.Value.DbName;
            options.UseInMemoryDatabase(inMemoryDbName);
        });

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }
}