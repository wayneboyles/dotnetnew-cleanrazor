using CleanRazor.Entities;

namespace CleanRazor.Dtos
{
    public abstract class AuditedEntityDto<TKey> : IEntity<TKey>, IAudited
    {
        public TKey Id { get; set; } = default!;

        public bool IsEnabled { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
