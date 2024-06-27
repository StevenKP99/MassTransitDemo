using Domain;
using Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.CompilerServices;

namespace Infrastructure.Interceptors;

public class AuditableItemUpdatedIntercepter : SaveChangesInterceptor
{
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is not null)
        {
            UpdateAuditableEntities(dbContext);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);

    }

    private void UpdateAuditableEntities(DbContext context)
    {
        var entities = context.ChangeTracker.Entries<AuditableEntity>().ToList();

        foreach(EntityEntry<AuditableEntity> entry in entities)
        {
            var changeDetails = entry.Properties
                .Where(prop => prop.IsModified && prop.CurrentValue is not null && !prop.CurrentValue.Equals(prop.OriginalValue))
                .Select(prop => $"{prop.Metadata.Name} {prop.OriginalValue} --> {prop.CurrentValue}")
                .ToArray();

            var updateEvent = new AuditbleEntityEvent(entry.State.ToString(), changeDetails, entry.Entity.UpdatedDateTime);

            entry.Entity.RaiseEvent(updateEvent);
        }
    }
}
