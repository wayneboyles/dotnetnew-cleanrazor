namespace CleanRazor.Entities
{
    public abstract class AuditedEntity<TKey> : Entity<TKey>, IAudited
    {
        public string? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
