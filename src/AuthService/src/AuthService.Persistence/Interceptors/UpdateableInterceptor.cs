using AuthService.Domain.SeedWork;
using AuthService.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AuthService.Persistence.Interceptors
{
    public class UpdateableInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            CheckUpdateableEntities(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            CheckUpdateableEntities(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void CheckUpdateableEntities(DbContextEventData eventData)
        {
            var entriesToBeUpdated = eventData.Context?.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            if (entriesToBeUpdated == null || entriesToBeUpdated.Count == 0)
                return;

            foreach (var entry in entriesToBeUpdated)
            {
                var entityType = entry.Entity.GetType();

                if (!entityType.IsAssignableTo(typeof(IUpdateable)))
                    throw new NonUpdateableEntityException(entityType.Name);

                entityType.GetProperty(nameof(IUpdateable.UpdatedDate))!.SetValue(entry.Entity, DateTime.UtcNow);
            }
        }
    }
}
