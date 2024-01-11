using System.Reflection;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<FileData> FileData { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
    {
        foreach (var entity 
                 in ChangeTracker
                     .Entries()
                     .Where(x => x is { Entity: BaseEntity, State: EntityState.Modified })
                     .Select(x => x.Entity)
                     .Cast<BaseEntity>()
                 )
            entity.LastModified = DateTime.UtcNow;
    
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
    }
}
