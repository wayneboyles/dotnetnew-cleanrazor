namespace CleanRazor.Entities
{
    /// <summary>
    /// Represents an entity with audit fields
    /// </summary>
    public interface IAudited
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
        public DateTimeOffset CreatedOn { get; set; }

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
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
