using CleanRazor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanRazor.EntityFrameworkCore.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context == null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete })
                {
                    continue;
                }

                entry.State = EntityState.Modified;

                delete.IsDeleted = true;
                delete.DeletedOn = DateTimeOffset.UtcNow;
            }

            return result;
        }
    }
}
