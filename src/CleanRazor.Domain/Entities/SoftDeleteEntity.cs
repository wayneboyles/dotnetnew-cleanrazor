using CleanRazor.Data;

namespace CleanRazor.Entities
{
    public abstract class SoftDeleteEntity<TKey> : Entity<TKey>, ISoftDelete
    {
        public bool IsDeleted { get; set; }

        public string? DeletedBy { get; set; }

        public DateTimeOffset? DeletedOn { get; set; }
    }
}
