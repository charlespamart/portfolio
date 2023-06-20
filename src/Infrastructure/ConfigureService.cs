using Application.Common.Interfaces;
using CommunityToolkit.Diagnostics;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        var databaseConnectionString = Environment.GetEnvironmentVariable("Database:ConnectionString");

        Guard.IsNotNullOrEmpty(databaseConnectionString);

        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(databaseConnectionString));

        return services.AddScoped<ITodoDbContext, TodoDbContext>();
    }
}