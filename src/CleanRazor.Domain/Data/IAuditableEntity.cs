namespace CleanRazor.Data
{
    /// <summary>
    /// Represents an entity with audit fields
    /// </summary>
    public interface IAuditableEntity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Who the entity was created by.
        /// </summary>
        /// <value>
        /// The entity creator.
        /// </value>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// When the entity was created.
        /// </summary>
        /// <value>
        /// The creation date and time.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Who the entity was modified by.
        /// </summary>
        /// <value>
        /// The entity modifier.
        /// </value>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// When the entity was last modified.
        /// </summary>
        /// <value>
        /// The modification date and time.
        /// </value>
        public DateTime? ModifiedOn { get; set; }
    }
}
