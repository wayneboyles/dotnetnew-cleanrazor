namespace CleanRazor.Data
{
    /// <summary>
    /// Used to implement soft deletes within the database
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Gets or sets a value indicating whether this entity is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this entity is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Who the entity was deleted by.
        /// </summary>
        /// <value>
        /// The name of the user that deleted the entity.
        /// </value>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// When the entity was deleted.
        /// </summary>
        /// <value>
        /// The deleted date and time.
        /// </value>
        public DateTime? DeletedOn { get; set; }
    }
}
