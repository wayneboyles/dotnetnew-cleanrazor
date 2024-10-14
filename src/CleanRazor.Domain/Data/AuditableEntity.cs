namespace CleanRazor.Data
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
