using Application.Common.Interfaces;
using Domain.OptionConfigurations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class ConfigureService
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddInMemoryTodoDbContext();
    }

    private static void AddPostgresTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var postgresDbOptions = serviceProvider.GetRequiredService<IOptions<PostgresDbOptions>>();
            var postgresDbConnectionString = postgresDbOptions.Value.ConnectionString;
            options.UseNpgsql(postgresDbConnectionString);
        }).AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static void AddMySqlTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var mySqlDbOptions = serviceProvider.GetRequiredService<IOptions<MySqlDbOptions>>();
            var mySqlDbConnectionString = mySqlDbOptions.Value.ConnectionString;
            options.UseMySQL(mySqlDbConnectionString);
        }).AddScoped<ITodoDbContext, TodoDbContext>();
    }

    private static void AddInMemoryTodoDbContext(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((serviceProvider, options) =>
        {
            var inMemoryDbOptions = serviceProvider.GetRequiredService<IOptions<InMemoryDbOptions>>();
            var inMemoryDbName = inMemoryDbOptions.Value.DbName;
            options.UseInMemoryDatabase(inMemoryDbName);
        }).AddScoped<ITodoDbContext, TodoDbContext>();
    }
}