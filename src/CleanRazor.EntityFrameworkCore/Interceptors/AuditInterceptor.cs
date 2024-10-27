using CleanRazor.Entities;
using CleanRazor.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanRazor.EntityFrameworkCore.Interceptors
{
    public sealed class AuditInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context == null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Added or EntityState.Modified, Entity: IAudited audit })
                {
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        audit.CreatedBy = currentUser.UserName;
                        audit.CreatedOn = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        audit.ModifiedBy = currentUser.UserName;
                        audit.ModifiedOn = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Detached:
                    case EntityState.Deleted:
                    case EntityState.Unchanged:
                        continue;
                }
            }

            return result;
        }
    }
}
