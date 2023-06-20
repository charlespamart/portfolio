using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface ITodoDbContext
{
    public DbSet<Todo> Todo { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}