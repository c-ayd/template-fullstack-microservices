using AuthService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AuthService.Persistence.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            CheckSoftDeletedEntities(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            CheckSoftDeletedEntities(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void CheckSoftDeletedEntities(DbContextEventData eventData)
        {
            var entriesToBeDeleted = eventData.Context?.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();

            if (entriesToBeDeleted == null || entriesToBeDeleted.Count == 0)
                return;

            foreach (var entry in entriesToBeDeleted)
            {
                var entityType = entry.Entity.GetType();
                if (!entityType.IsAssignableTo(typeof(ISoftDelete)))
                    continue;

                var isDeletedProperty = entityType.GetProperty(nameof(ISoftDelete.IsDeleted))!;
                var isDeleted = (bool)isDeletedProperty.GetValue(entry.Entity)!;
                if (isDeleted)
                    continue;

                isDeletedProperty.SetValue(entry.Entity, true);
                entityType.GetProperty(nameof(ISoftDelete.DeletedDate))!.SetValue(entry.Entity, DateTimeOffset.UtcNow);

                entry.State = EntityState.Modified;
            }
        }
    }
}
