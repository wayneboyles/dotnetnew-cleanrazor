using CleanRazor.Data;

namespace CleanRazor.Entities
{
    public abstract class SoftDeleteAuditedEntity<TKey> : AuditedEntity<TKey>, ISoftDelete
    {
        public bool IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTimeOffset? DeletedOn { get; set; }
    }
}
